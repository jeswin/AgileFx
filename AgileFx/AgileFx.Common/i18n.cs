/* AgileFx Version 2.0
 * An open-source framework for rapid development of applications and services using Microsoft.net
 * Developed by: AgileHead
 * Website: www.agilefx.org
 * This component is licensed under the terms of the Apache 2.0 License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgileFx
{
    public interface II18nHandler
    {
        string GetString(string code);
    }

    public class I18n
    {
        public static Func<II18nHandler> DefaultHandler;

        II18nHandler _handler;
        public II18nHandler Handler
        {
            get
            {
                if (_handler != null)
                    return _handler;
                else if (DefaultHandler != null)
                    return DefaultHandler();
                else
                    throw new Exception("A handler of the type II18nHandler needs to be passed as an argument to the I18n constructor.");                    
            }
        }

        public I18n()
        {
        }

        public I18n(II18nHandler handler)
        {
            this._handler = handler;
        }
        
        public static string GetString(string code)
        {
            return new I18n().Handler.GetString(code);
        }

        public static string GetString(string code, string[] args)
        {
            return string.Format(new I18n().Handler.GetString(code), args);
        }

        public static void Configure(Func<II18nHandler> I18nHandlerFetcher)
        {
            DefaultHandler = I18nHandlerFetcher;
        }
    }
}