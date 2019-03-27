﻿///
/// Copyright 2017 ID&Trust, Ltd.
///
/// You are hereby granted a non-exclusive, worldwide, royalty-free license to
/// use, copy, modify, and distribute this software in source code or binary form
/// for use in connection with the web services and APIs provided by ID&Trust.
///
/// As with any software that integrates with the GoodID platform, your use
/// of this software is subject to the GoodID Terms of Service
/// (https://goodid.net/docs/tos).
/// This copyright notice shall be included in all copies or substantial portions
/// of the software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
/// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
/// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
/// DEALINGS IN THE SOFTWARE.
///
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using GoodId.Core.Exceptions;

namespace GoodId.Core.Helpers.ClaimChecker
{
    class GoodIDAppUserChecker : IClaimChecker
    {
        private const string ClaimName = "user";

        private JObject idToken;

        public GoodIDAppUserChecker(JObject idToken)
        {
            this.idToken = idToken;
        }

        public void CheckClaim(JToken token)
        {

            Acr acr = idToken["acr"].ToObject<Acr>();
            string user = token.Value<string>();

            if(acr < Acr.LEVEL_3)
            {
                if( string.IsNullOrEmpty(user) == false)
                {
                    throw new ValidationException("Unverifiable user claim");

                }
                return; 
            }
            if (string.IsNullOrEmpty(user) == true)
            {
                throw new ValidationException("Missing user claim");
            }
        }

        public string SupportedClaim()
        {
            return ClaimName;
        }
    }
}