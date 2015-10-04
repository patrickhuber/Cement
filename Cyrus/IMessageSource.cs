﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cyrus
{
    public interface IMessageSource : IDisposable
    {
        IDictionary<string, string> MessageHeader { get; }
        int Read(byte[] buffer, int offset, int count);
        Task<int> ReadAsync(byte[] buffer, int offset, int count);
        void Close();
    }
}