using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml.Linq;
using System.Diagnostics;
using System.Reflection;

namespace Integration.IO
{
    /// <summary>
    /// Visits nodes in an expression tree and builds an xml representation.
    /// </summary>
    public class XmlExpressionVisitor : ExpressionVisitor
    {
        private Stack<XElement> elementStack;
        private int depth;

        /// <summary>
        /// Automatics the XML.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public XElement ToXml(Expression expression)
        {
            this.Visit(expression);
            return elementStack.Pop();
        }

        /// <summary>
        /// Dispatches the expression to one of the more specialized visit methods in this class.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        public override Expression Visit(Expression node)
        {
            // keep a depth count so that we know when to reset everything
            if (depth == 0)
            {
                elementStack = new Stack<XElement>();
                elementStack.Push(new XElement("expression"));
            }
#if DEBUG
            Console.WriteLine(string.Format(
                "{1}type: {0}", 
                node == null 
                    ? "null" 
                    : Enum.GetName(typeof(ExpressionType), node.NodeType), 
                string.Concat(Enumerable.Repeat("   ", depth))));
#endif            
            depth++;
            Expression returnExpression = base.Visit(node);
            depth--;
            return returnExpression;
        }

        /// <summary>
        /// Visits the lambda.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            XElement body = new XElement("body");
            XElement parameters = new XElement("parameters");
            XElement lambda = new XElement("lambda", body, parameters);

            elementStack.Peek().Add(lambda);

            elementStack.Push(body);
            var visitBody = base.Visit(node.Body);
            elementStack.Pop();

            elementStack.Push(parameters);
            var visitParameters = (IEnumerable<ParameterExpression>)this.VisitAndConvert<ParameterExpression>(node.Parameters, "VisitLambda");
            elementStack.Pop();
            // assign the current element to the body of the lambda expression                        

            var result =  (Expression)node.Update(
                   visitBody,
                   visitParameters);

            return result;
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.MemberInitExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            XElement bindings = new XElement("bindings");
            XElement memberInitExpression = new XElement("memberInit", bindings);

            elementStack.Peek().Add(memberInitExpression);

            elementStack.Push(memberInitExpression);
            var newExpression = this.VisitAndConvert<NewExpression>(node.NewExpression, "VisitMemberInit");
            elementStack.Pop();

            elementStack.Push(bindings);
            var parametersExpression = (IEnumerable<MemberBinding>)ExpressionVisitor.Visit<MemberBinding>(node.Bindings, new Func<MemberBinding, MemberBinding>(this.VisitMemberBinding));
            elementStack.Pop();

            return (Expression)node.Update(
                newExpression, 
                parametersExpression);    
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.NewExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitNew(NewExpression node)
        {
            XElement arguments = new XElement("arguments");
            XElement newExpression = new XElement("new", arguments);
            
            elementStack.Peek().Add(newExpression);
            elementStack.Push(newExpression);
            elementStack.Push(arguments);
            var returnExpression = (Expression)node.Update((IEnumerable<Expression>)this.Visit(node.Arguments));
            elementStack.Pop();
            elementStack.Pop();
            return returnExpression;
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.MemberAssignment" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            XElement memberAssignment = new XElement("memberAssignment");            
            elementStack.Peek().Add(memberAssignment);
            elementStack.Push(memberAssignment);
            var returnExpression = base.VisitMemberAssignment(node);
            elementStack.Pop();
            return returnExpression;
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.MemberExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitMember(MemberExpression node)
        {
            XElement member = new XElement("member",
                    new XAttribute("name", node.Member.Name),
                    new XAttribute("declaringType", node.Member.DeclaringType.FullName),
                    new XAttribute("memberType", Enum.GetName(typeof(MemberTypes), node.Member.MemberType)));

            XElement memberAccess = new XElement("memberAccess",
                member);

            switch (node.Member.MemberType)
            { 
                case MemberTypes.Property:
                    member.Add(new XAttribute("type", (node.Member as PropertyInfo).PropertyType));
                    break;
                case MemberTypes.Field:
                    member.Add(new XAttribute("type", (node.Member as FieldInfo).FieldType));
                    break;
            }
            elementStack.Peek().Add(memberAccess);
            elementStack.Push(memberAccess);
            var returnExpression = base.VisitMember(node);
            elementStack.Pop();
            return returnExpression;
        }

        /// <summary>
        /// Visits the children of the <see cref="T:System.Linq.Expressions.MethodCallExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            XElement method = new XElement("method",
                new XAttribute("name", node.Method.Name),
                new XAttribute("declaringType", node.Method.DeclaringType));
            XElement arguments = new XElement("arguments");
            XElement methodCall = new XElement("call", 
                method,
                arguments);

            elementStack.Peek().Add(methodCall);
            elementStack.Push(methodCall);
            Expression instance = this.Visit(node.Object);

            int argumentCount = node.Arguments.Count;

            // the list of visited argument expressions
            Expression[] expressionArray = (Expression[])null;
            elementStack.Push(arguments);

            // I believe this is doing argument consolidation
            for (int argumentIndex = 0; argumentIndex < node.Arguments.Count; ++argumentIndex)
            {
                // set the argument expression to the current arguments expression
                // set the visit exprssion to the visited argument expression
                Expression argumentExpression = node.Arguments[argumentIndex];
                Expression argumentVisitExpression = this.Visit(argumentExpression);

                // if the argument array has been initialized, use the visit expression and set to the current
                // argument index
                if (expressionArray != null)
                    expressionArray[argumentIndex] = argumentVisitExpression;

                // if the visited expression is the same as the input expression
                else if (!object.ReferenceEquals((object)argumentVisitExpression, (object)argumentExpression))
                {
                    // create the expression array with the size of the argument count
                    expressionArray = new Expression[argumentCount];

                    // loop over all the arguments and set their place in the array
                    for (int secondaryArgumentIndex = 0; secondaryArgumentIndex < argumentIndex; ++secondaryArgumentIndex)                    
                        expressionArray[secondaryArgumentIndex] = node.Arguments[secondaryArgumentIndex];
                    
                    // set the argument's index to the visited expression index
                    expressionArray[argumentIndex] = argumentVisitExpression;
                }
            }
            elementStack.Pop();
            elementStack.Pop();

            // we now have a list of arguments
            if (instance == node.Object && expressionArray == null)
            {                
                return (Expression)node;
            }
            else
                throw new Exception("Unreachable code detected.");            
        }

        /// <summary>
        /// Visits the <see cref="T:System.Linq.Expressions.ConstantExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitConstant(ConstantExpression node)
        {
            XElement constant = new XElement("constant",
                new XAttribute("value", node.Value));            
            elementStack.Peek().Add(constant);
            elementStack.Push(constant);
            var returnExpression = base.VisitConstant(node);
            elementStack.Pop();
            return returnExpression;
        }

        /// <summary>
        /// Visits the <see cref="T:System.Linq.Expressions.ParameterExpression" />.
        /// </summary>
        /// <param name="node">The expression to visit.</param>
        /// <returns>
        /// The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.
        /// </returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            XElement parameter = new XElement("parameter",
                new XAttribute("name", node.Name),
                new XAttribute("isByRef", node.IsByRef));            
            
            elementStack.Peek().Add(parameter);
            elementStack.Push(parameter);
            var returnExpression = base.VisitParameter(node);
            elementStack.Pop();
            return returnExpression;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {            
            return base.VisitUnary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            XElement leftElement = new XElement("left");
            XElement rightElement = new XElement("right");
            XElement binaryElement = new XElement("binary", leftElement, rightElement);
            elementStack.Push(binaryElement);
            var result = base.VisitBinary(node);
            elementStack.Pop();
            return result;
        }
    }
}
