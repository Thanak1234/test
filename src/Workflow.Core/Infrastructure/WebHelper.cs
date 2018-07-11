using System;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using Workflow.Collections;
//using Workflow.Core.Data;
//using Workflow.Core.Domain.Stores;
//using Workflow.Core.Infrastructure;
using Workflow.Core;

namespace Workflow.Core
{

    public partial class WebHelper : IWebHelper
    {
		private static bool? s_optimizedCompilationsEnabled = null;
		private static AspNetHostingPermissionLevel? s_trustLevel = null;
		private static readonly Regex s_staticExts = new Regex(@"(.*?)\.(css|js|png|jpg|jpeg|gif|bmp|html|htm|xml|pdf|doc|xls|rar|zip|ico|eot|svg|ttf|woff|otf|axd|ashx|less)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		private static readonly Regex s_htmlPathPattern = new Regex(@"(?<=(?:href|src)=(?:""|'))(?!https?://)(?<url>[^(?:""|')]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);
		private static readonly Regex s_cssPathPattern = new Regex(@"url\('(?<url>.+)'\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

		private readonly HttpContextBase _httpContext;
        private bool? _isCurrentConnectionSecured;
		//private bool? _appPathPossiblyAppended;
		//private bool? _appPathPossiblyAppendedSsl;

        public WebHelper() {

        }

        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        public virtual string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;

            if (_httpContext != null &&
                _httpContext.Request != null &&
                _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.ToString();

            return referrerUrl;
        }

        public virtual string GetCurrentIpAddress()
        {
			string result = null;

			if (_httpContext != null && _httpContext.Request != null)
				result = _httpContext.Request.UserHostAddress;

			if (result == "::1")
				result = "127.0.0.1";

			return result.EmptyNull();
        }
        
        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetThisPageUrl(includeQueryString, useSsl);
        }

        public virtual string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            return string.Empty;
        }

        public virtual bool IsCurrentConnectionSecured()
        {
            if (!_isCurrentConnectionSecured.HasValue)
            {
                _isCurrentConnectionSecured = false;
                if (_httpContext != null && _httpContext.Request != null)
                {
                    _isCurrentConnectionSecured = _httpContext.Request.IsSecureConnection();
                }
            }

            return _isCurrentConnectionSecured.Value;
        }
        
        public virtual string ServerVariables(string name)
        {
            string result = string.Empty;

            try
            {
				if (_httpContext != null && _httpContext.Request != null)
				{
					if (_httpContext.Request.ServerVariables[name] != null)
					{
						result = _httpContext.Request.ServerVariables[name];
					}
				}
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        private string GetHostPart(string url)
        {
            var uri = new Uri(url);
            var host = uri.GetComponents(UriComponents.Scheme | UriComponents.Host, UriFormat.Unescaped);
            return host;
        }
        
        public virtual string GetStoreLocation()
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetStoreLocation(useSsl);
        }

        public virtual string GetStoreLocation(bool useSsl)
        {
            return string.Empty;
        }
        
        public virtual bool IsStaticResource(HttpRequest request)
        {
			return IsStaticResourceRequested(new HttpRequestWrapper(request));
        }

		public static bool IsStaticResourceRequested(HttpRequest request)
		{
			Guard.ArgumentNotNull(() => request);
			return s_staticExts.IsMatch(request.Path);
		}

		public static bool IsStaticResourceRequested(HttpRequestBase request)
		{
			// unit testable
			Guard.ArgumentNotNull(() => request);
			return s_staticExts.IsMatch(request.Path);
		}
        
        public virtual string MapPath(string path)
        {
			return CommonHelper.MapPath(path, false);
        }
        
        public virtual string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
			// TODO: routine should not return a query string in lowercase (unless the caller is telling him to do so).
			url = url.EmptyNull().ToLower();
			queryStringModification = queryStringModification.EmptyNull().ToLower();

			string curAnchor = null;

			var hsIndex = url.LastIndexOf('#');
			if (hsIndex >= 0)
			{
				curAnchor = url.Substring(hsIndex);
				url = url.Substring(0, hsIndex);
			}
			
			var parts = url.Split(new[] { '?' });
			var current = new QueryString(parts.Length == 2 ? parts[1] : "");
			var modify = new QueryString(queryStringModification);

			foreach (var nv in modify.AllKeys)
			{
				current.Add(nv, modify[nv], true);
			}

			var result = "{0}{1}{2}".FormatCurrent(parts[0], current.ToString(), anchor.NullEmpty() == null ? (curAnchor == null ? "" : "#" + curAnchor.ToLower()) : "#" + anchor.ToLower());
			return result;
        }

        public virtual string RemoveQueryString(string url, string queryString)
        {
			var parts = url.EmptyNull().ToLower().Split(new[] { '?' });
			var current = new QueryString(parts.Length == 2 ? parts[1] : "");

			if (current.Count > 0 && queryString.HasValue())
			{
				current.Remove(queryString);
			}

			var result = "{0}{1}".FormatCurrent(parts[0], current.ToString());
			return result;
        }
        
        public virtual T QueryString<T>(string name)
        {
            string queryParam = null;
            if (_httpContext != null && _httpContext.Request.QueryString[name] != null)
                queryParam = _httpContext.Request.QueryString[name];

            if (!String.IsNullOrEmpty(queryParam))
                return queryParam.Convert<T>();

            return default(T);
        }
        
        public virtual void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "", bool aggressive = false)
        {
			HttpRuntime.UnloadAppDomain();

			if (aggressive)
			{
				TryWriteBinFolder();
			}
			else
			{
				// without this, MVC may fail resolving controllers for newly installed plugins after IIS restart
				Thread.Sleep(250);
			}

            // If setting up plugins requires an AppDomain restart, it's very unlikely the
            // current request can be processed correctly.  So, we redirect to the same URL, so that the
            // new request will come to the newly started AppDomain.
            if (_httpContext != null && makeRedirect)
            {
				if (_httpContext.Request.RequestType == "GET")
				{
					if (String.IsNullOrEmpty(redirectUrl))
					{
						redirectUrl = GetThisPageUrl(true);
					}
					_httpContext.Response.Redirect(redirectUrl, true /*endResponse*/);
				}
				else
				{
					// Don't redirect posts...
					_httpContext.Response.ContentType = "text/html";
					_httpContext.Response.WriteFile("~/refresh.html");
					_httpContext.Response.End();
				}
            }
        }

        private bool TryWriteWebConfig()
        {
            try
            {
                // In medium trust, "UnloadAppDomain" is not supported. Touch web.config
                // to force an AppDomain restart.
                File.SetLastWriteTimeUtc(MapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TryWriteGlobalAsax()
        {
            try
            {
                //When a new plugin is dropped in the Plugins folder and is installed into SmartSTore.NET, 
                //even if the plugin has registered routes for its controllers, 
                //these routes will not be working as the MVC framework can't
                //find the new controller types in order to instantiate the requested controller. 
                //That's why you get these nasty errors 
                //i.e "Controller does not implement IController".
                //The solution is to touch the 'top-level' global.asax file
                File.SetLastWriteTimeUtc(MapPath("~/global.asax"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

		private bool TryWriteBinFolder()
		{
			try
			{
				var binMarker = MapPath("~/bin/HostRestart");
				Directory.CreateDirectory(binMarker);

				using (var stream = File.CreateText(Path.Combine(binMarker, "marker.txt")))
				{
					stream.WriteLine("Restart on '{0}'", DateTime.UtcNow);
					stream.Flush();
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		internal static bool OptimizedCompilationsEnabled
		{
			get
			{
				if (!s_optimizedCompilationsEnabled.HasValue)
				{
					var section = (CompilationSection)ConfigurationManager.GetSection("system.web/compilation");
					s_optimizedCompilationsEnabled = section.OptimizeCompilations;
				}

				return s_optimizedCompilationsEnabled.Value;
			}
		}

        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContext.Response;
                return response.IsRequestBeingRedirected;   
            }
        }

        public virtual bool IsPostBeingDone
        {
            get
            {
                if (_httpContext.Items["sm.IsPOSTBeingDone"] == null)
                    return false;
                return Convert.ToBoolean(_httpContext.Items["sm.IsPOSTBeingDone"]);
            }
            set
            {
                _httpContext.Items["sm.IsPOSTBeingDone"] = value;
            }
        }

		/// <summary>
		/// Finds the trust level of the running application (http://blogs.msdn.com/dmitryr/archive/2007/01/23/finding-out-the-current-trust-level-in-asp-net.aspx)
		/// </summary>
		/// <returns>The current trust level.</returns>
		public static AspNetHostingPermissionLevel GetTrustLevel()
		{
			if (!s_trustLevel.HasValue)
			{
				//set minimum
				s_trustLevel = AspNetHostingPermissionLevel.None;

				//determine maximum
				foreach (AspNetHostingPermissionLevel trustLevel in
						new AspNetHostingPermissionLevel[] {
                                AspNetHostingPermissionLevel.Unrestricted,
                                AspNetHostingPermissionLevel.High,
                                AspNetHostingPermissionLevel.Medium,
                                AspNetHostingPermissionLevel.Low,
                                AspNetHostingPermissionLevel.Minimal 
                            })
				{
					try
					{
						new AspNetHostingPermission(trustLevel).Demand();
						s_trustLevel = trustLevel;
						break; //we've set the highest permission we can
					}
					catch (System.Security.SecurityException)
					{
						continue;
					}
				}
			}
			return s_trustLevel.Value;
		}

		/// <summary>
		/// Prepends protocol and host to all (relative) urls in a html string
		/// </summary>
		/// <param name="html">The html string</param>
		/// <param name="request">Request object</param>
		/// <returns>The transformed result html</returns>
		/// <remarks>
		/// All html attributed named <c>src</c> and <c>href</c> are affected, also occurences of <c>url('path')</c> within embedded stylesheets.
		/// </remarks>
		public static string MakeAllUrlsAbsolute(string html, HttpRequestBase request)
		{
			Guard.ArgumentNotNull(() => request);

			if (request.Url == null)
			{
				return html;
			}

			return MakeAllUrlsAbsolute(html, request.Url.Scheme, request.Url.Authority);
		}

		/// <summary>
		/// Prepends protocol and host to all (relative) urls in a html string
		/// </summary>
		/// <param name="html">The html string</param>
		/// <param name="protocol">The protocol to prepend, e.g. <c>http</c></param>
		/// <param name="host">The host name to prepend, e.g. <c>www.mysite.com</c></param>
		/// <returns>The transformed result html</returns>
		/// <remarks>
		/// All html attributed named <c>src</c> and <c>href</c> are affected, also occurences of <c>url('path')</c> within embedded stylesheets.
		/// </remarks>
		public static string MakeAllUrlsAbsolute(string html, string protocol, string host)
		{
			Guard.ArgumentNotEmpty(() => html);
			Guard.ArgumentNotEmpty(() => protocol);
			Guard.ArgumentNotEmpty(() => host);

			string baseUrl = string.Format("{0}://{1}", protocol, host.TrimEnd('/'));

			MatchEvaluator evaluator = (match) =>
			{
				var url = match.Groups["url"].Value;
				return "{0}{1}".FormatCurrent(baseUrl, url.EnsureStartsWith("/"));
			};

			html = s_htmlPathPattern.Replace(html, evaluator);
			html = s_cssPathPattern.Replace(html, evaluator);

			return html;
		}

		/// <summary>
		/// Prepends protocol and host to the given (relative) url
		/// </summary>
		public static string GetAbsoluteUrl(string url, HttpRequestBase request)
		{
			Guard.ArgumentNotEmpty(() => url);
			Guard.ArgumentNotNull(() => request);

			if (request.Url == null)
			{
				return url;
			}

			if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				return url;
			}

			if (url.StartsWith("~"))
			{
				url = VirtualPathUtility.ToAbsolute(url);
			}

			url = String.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url);
			return url;
		}

        private class StoreHost
        {
            public string Host { get; set; }
            public bool ExpectingDirtySecurityChannelMove { get; set; }
        }

    }
}