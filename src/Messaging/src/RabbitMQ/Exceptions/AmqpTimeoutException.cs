﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Steeltoe.Messaging.Rabbit.
    Exceptions
{
    public class AmqpTimeoutException : AmqpException
    {
        public AmqpTimeoutException(string message, Exception cause)
        : base(message, cause)
        {
        }

        public AmqpTimeoutException(string message)
        : base(message)
        {
        }

        public AmqpTimeoutException(Exception cause)
        : base(cause)
        {
        }
    }
}
