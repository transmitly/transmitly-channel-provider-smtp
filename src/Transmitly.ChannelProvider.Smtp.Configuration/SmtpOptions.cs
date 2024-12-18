// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
//  
//  Licensed under the Apache License, Version 2.0 (the "License")
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System.Net;
using System.Text;

namespace Transmitly.ChannelProvider.Smtp.Configuration
{
    public sealed class SmtpOptions
    {
        public SecureSocketOptions SocketOptions { get; set; }

        public string? Host { get; set; }
        public int? Port { get; set; }
        public Encoding Encoding { get; set; } = Encoding.UTF8;
        public ICredentials? Credentials { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
