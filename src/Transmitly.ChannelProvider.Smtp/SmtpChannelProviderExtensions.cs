// Copyright (c) Code Impressions, LLC. All Rights Reserved.
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

using System;
using Transmitly.ChannelProvider.Smtp.Configuration;
using Transmitly.ChannelProvider.Smtp.MailKit;
using Transmitly.Util;

namespace Transmitly
{
    /// <summary>
    /// Provides extension methods for registering and identifying the SMTP channel provider.
    /// </summary>
    public static class SmtpChannelProviderExtensions
    {
        private const string SmtpId = "MailKit";

        /// <summary>
        /// Gets the SMTP provider identifier for the specified channel provider collection.
        /// </summary>
        /// <param name="channelProviders">The channel provider collection.</param>
        /// <param name="providerId">An optional provider identifier suffix.</param>
        /// <returns>The resolved SMTP provider identifier.</returns>
        public static string Smtp(this ChannelProviders channelProviders, string? providerId = null)
        {
            Guard.AgainstNull(channelProviders);
            return channelProviders.GetId(SmtpId, providerId);
        }

        /// <summary>
        /// Adds SMTP email support to the communications client builder.
        /// </summary>
        /// <param name="communicationsClientBuilder">The communications client builder.</param>
        /// <param name="options">Configures SMTP options for the provider.</param>
        /// <param name="providerId">An optional provider identifier suffix.</param>
        /// <returns>The same <see cref="CommunicationsClientBuilder"/> instance.</returns>
        public static CommunicationsClientBuilder AddSmtpSupport(this CommunicationsClientBuilder communicationsClientBuilder, Action<SmtpOptions> options, string? providerId = null)
        {
            var optionObj = new SmtpOptions();
            options(optionObj);

            communicationsClientBuilder.ChannelProvider.Add<MailKitChannelProviderClient, IEmail>(Id.ChannelProvider.Smtp(providerId), optionObj, Id.Channel.Email());
            return communicationsClientBuilder;
        }
    }
}