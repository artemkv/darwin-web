﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;

namespace Darwin
{
	public class DarwinXmlMediaTypeFormatter : XmlMediaTypeFormatter
	{
		public DarwinXmlMediaTypeFormatter()
			: base()
		{
			this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/artemkv.darwindb+xml"));
		}
	}
}