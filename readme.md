# Cyrus Framework

Named after Cyrus the Great who created the first real postal system.

## Overview

Cyrus is a implementation of Enterprise Integration Patterns. 

## Adapters

Adapters provide interfaces to Applications in order to abstract the origination and destination of messages. 

Two main Adapter interfaces exist:

* ISendAdapter - Pulls a message from a channel and sends it to the target.
* IReceiveAdapter - Receives a message from the source and sends it to a channel.

## Channels