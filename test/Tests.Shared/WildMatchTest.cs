#region Copyright
/*******************************************************************************
 * NerdyDuck.Tests.DotIgnore - Unit tests for the
 * NerdyDuck.DotIgnore assembly
 * 
 * The MIT License (MIT)
 *
 * Copyright (c) Daniel Kopp, dak@nerdyduck.de
 *
 * All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ******************************************************************************/
#endregion

using NerdyDuck.DotIgnore.Native;

namespace NerdyDuck.Tests.DotIgnore
{
	[ExcludeFromCodeCoverage]
	[TestClass]
	public class WildMatchTest
	{
		#region Basic wildmatch features
		[TestMethod]
		public void WildMatchBasic01()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo", "foo"));
		}

		[TestMethod]
		public void WildMatchBasic02()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "bar"));
		}

		[TestMethod]
		public void WildMatchBasic03()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("", ""));
		}

		[TestMethod]
		public void WildMatchBasic04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo", "???"));
		}

		[TestMethod]
		public void WildMatchBasic05()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "??"));
		}

		[TestMethod]
		public void WildMatchBasic06()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo", "*"));
		}

		[TestMethod]
		public void WildMatchBasic07()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo", "f*"));
		}

		[TestMethod]
		public void WildMatchBasic08()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "*f"));
		}

		[TestMethod]
		public void WildMatchBasic09()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo", "*foo*"));
		}

		[TestMethod]
		public void WildMatchBasic10()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foobar", "*ob*a*r*"));
		}

		[TestMethod]
		public void WildMatchBasic11()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("aaaaaaabababab", "*ab"));
		}

		[TestMethod]
		public void WildMatchBasic12()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo*", "foo*"));
		}

		[TestMethod]
		public void WildMatchBasic13()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foobar", @"foo\*bar"));
		}

		[TestMethod]
		public void WildMatchBasic14()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(@"f\oo", @"f\\oo"));
		}

		[TestMethod]
		public void WildMatchBasic15()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("ball", "*[al]?"));
		}

		[TestMethod]
		public void WildMatchBasic16()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ten", "[ten]"));
		}

		[TestMethod]
		public void WildMatchBasic17()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("ten", "**[!te]"));
		}

		[TestMethod]
		public void WildMatchBasic18()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ten", "**[!ten]"));
		}

		[TestMethod]
		public void WildMatchBasic19()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("ten", "t[a-g]n"));
		}

		[TestMethod]
		public void WildMatchBasic20()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ten", "t[!a-g]n"));
		}

		[TestMethod]
		public void WildMatchBasic21()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("ton", "t[!a-g]n"));
		}

		[TestMethod]
		public void WildMatchBasic22()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("ton", "t[^a-g]n"));
		}

		[TestMethod]
		public void WildMatchBasic23()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a]b", "a[]]b"));
		}

		[TestMethod]
		public void WildMatchBasic24()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a-b", "a[]-]b"));
		}

		[TestMethod]
		public void WildMatchBasic25()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a]b", "a[]-]b"));
		}

		[TestMethod]
		public void WildMatchBasic26()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("aab", "a[]-]b"));
		}

		[TestMethod]
		public void WildMatchBasic27()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("aab", "a[]a-]b"));
		}

		[TestMethod]
		public void WildMatchBasic28()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("]", "]"));
		}
		#endregion

		#region Extended slash-matching features
		[TestMethod]
		public void WildMatchSlash01()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/baz/bar", "foo*bar"));
		}

		[TestMethod]
		public void WildMatchSlash02()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/baz/bar", "foo**bar"));
		}

		[TestMethod]
		public void WildMatchSlash03()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foobazbar", "foo**bar"));
		}

		[TestMethod]
		public void WildMatchSlash04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/baz/bar", "foo/**/bar"));
		}

		[TestMethod]
		public void WildMatchSlash05()
		{
			Assert.AreEqual("1 1 0 0", TestMatchVariants("foo/baz/bar", "foo/**/**/bar"));
		}

		[TestMethod]
		public void WildMatchSlash06()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/b/a/z/bar", "foo/**/bar"));
		}

		[TestMethod]
		public void WildMatchSlash07()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/b/a/z/bar", "foo/**/**/bar"));
		}

		[TestMethod]
		public void WildMatchSlash08()
		{
			Assert.AreEqual("1 1 0 0", TestMatchVariants("foo/bar", "foo/**/bar"));
		}

		[TestMethod]
		public void WildMatchSlash09()
		{
			Assert.AreEqual("1 1 0 0", TestMatchVariants("foo/bar", "foo/**/**/bar"));
		}

		[TestMethod]
		public void WildMatchSlash10()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "foo?bar"));
		}

		[TestMethod]
		public void WildMatchSlash11()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "foo[/]bar"));
		}

		[TestMethod]
		public void WildMatchSlash12()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "foo[^a-z]bar"));
		}

		[TestMethod]
		public void WildMatchSlash13()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "f[^eiu][^eiu][^eiu][^eiu][^eiu]r"));
		}

		[TestMethod]
		public void WildMatchSlash14()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo-bar", "f[^eiu][^eiu][^eiu][^eiu][^eiu]r"));
		}

		[TestMethod]
		public void WildMatchSlash15()
		{
			Assert.AreEqual("1 1 0 0", TestMatchVariants("foo", "**/foo"));
		}

		[TestMethod]
		public void WildMatchSlash16()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("/foo", "**/foo"));
		}

		[TestMethod]
		public void WildMatchSlash17()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("bar/baz/foo", "**/foo"));
		}

		[TestMethod]
		public void WildMatchSlash18()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("bar/baz/foo", "*/foo"));
		}

		[TestMethod]
		public void WildMatchSlash19()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar/baz", "**/bar*"));
		}

		[TestMethod]
		public void WildMatchSlash20()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("deep/foo/bar/baz", "**/bar/*"));
		}

		[TestMethod]
		public void WildMatchSlash21()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("deep/foo/bar/baz/", "**/bar/*"));
		}

		[TestMethod]
		public void WildMatchSlash22()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("deep/foo/bar/baz/", "**/bar/**"));
		}

		[TestMethod]
		public void WildMatchSlash23()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("deep/foo/bar", "**/bar/*"));
		}

		[TestMethod]
		public void WildMatchSlash24()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("deep/foo/bar/", "**/bar/**"));
		}

		[TestMethod]
		public void WildMatchSlash25()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar/baz", "**/bar**"));
		}

		[TestMethod]
		public void WildMatchSlash26()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bar/baz/x", "*/bar/**"));
		}

		[TestMethod]
		public void WildMatchSlash27()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("deep/foo/bar/baz/x", "*/bar/**"));
		}

		[TestMethod]
		public void WildMatchSlash28()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("deep/foo/bar/baz/x", "**/bar/*/*"));
		}
		#endregion

		#region Various additional tests
		[TestMethod]
		public void WildMatchVarious01()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("acrt", "a[c-c]st"));
		}

		[TestMethod]
		public void WildMatchVarious02()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("acrt", "a[c-c]rt"));
		}

		[TestMethod]
		public void WildMatchVarious03()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("]", "[!]-]"));
		}

		[TestMethod]
		public void WildMatchVarious04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a", "[!]-]"));
		}

		[TestMethod]
		public void WildMatchVarious05()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("", @"\"));
		}

		[TestMethod]
		public void WildMatchVarious06()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(@"\", @"\")); // 1 1 1 1
		}

		[TestMethod]
		public void WildMatchVarious07()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(@"/\", @"*/\"));
		}

		[TestMethod]
		public void WildMatchVarious08()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(@"/\", @"*/\\"));
		}

		[TestMethod]
		public void WildMatchVarious09()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo", "foo"));
		}

		[TestMethod]
		public void WildMatchVarious10()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("@foo", "@foo"));
		}

		[TestMethod]
		public void WildMatchVarious11()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "@foo"));
		}

		[TestMethod]
		public void WildMatchVarious12()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[ab]", @"\[ab]"));
		}

		[TestMethod]
		public void WildMatchVarious13()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[ab]", "[[]ab]"));
		}

		[TestMethod]
		public void WildMatchVarious14()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[ab]", "[[:]ab]"));
		}

		[TestMethod]
		public void WildMatchVarious15()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("[ab]", "[[::]ab]"));
		}

		[TestMethod]
		public void WildMatchVarious16()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[ab]", "[[:digit]ab]"));
		}

		[TestMethod]
		public void WildMatchVarious17()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[ab]", @"[\[:]ab]"));
		}

		[TestMethod]
		public void WildMatchVarious18()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("?a?b", @"\??\?b"));
		}

		[TestMethod]
		public void WildMatchVarious19()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("abc", @"\a\b\c"));
		}

		[TestMethod]
		public void WildMatchVarious20()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "")); // E E E E
		}

		[TestMethod]
		public void WildMatchVarious21()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bar/baz/to", "**/t[o]"));
		}
		#endregion

		#region Character class tests
		[TestMethod]
		public void WildMatchCharClass01()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a1B", "[[:alpha:]][[:digit:]][[:upper:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass02()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("a", "[[:digit:][:upper:][:space:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass03()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("A", "[[:digit:][:upper:][:space:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("1", "[[:digit:][:upper:][:space:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass05()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("1", "[[:digit:][:upper:][:spaci:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass06()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(" ", "[[:digit:][:upper:][:space:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass07()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(".", "[[:digit:][:upper:][:space:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass08()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(".", "[[:digit:][:punct:][:space:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass09()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("5", "[[:xdigit:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass10()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("f", "[[:xdigit:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass11()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("D", "[[:xdigit:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass12()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("_", "[[:alnum:][:alpha:][:blank:][:cntrl:][:digit:][:graph:][:lower:][:print:][:punct:][:space:][:upper:][:xdigit:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass13()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(".", "[^[:alnum:][:alpha:][:blank:][:cntrl:][:digit:][:lower:][:space:][:upper:][:xdigit:]]"));
		}

		[TestMethod]
		public void WildMatchCharClass14()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("5", "[a-c[:digit:]x-z]"));
		}

		[TestMethod]
		public void WildMatchCharClass15()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("b", "[a-c[:digit:]x-z]"));
		}

		[TestMethod]
		public void WildMatchCharClass16()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("y", "[a-c[:digit:]x-z]"));
		}

		[TestMethod]
		public void WildMatchCharClass17()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("q", "[a-c[:digit:]x-z]"));
		}
		#endregion

		#region Additional tests, including some malformed wildmatch patterns
		[TestMethod]
		public void WildMatchAdditional01()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("]", @"[\\-^]"));
		}

		[TestMethod]
		public void WildMatchAdditional02()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("[", @"[\\-^]"));
		}

		[TestMethod]
		public void WildMatchAdditional03()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", @"[\-_]"));
		}

		[TestMethod]
		public void WildMatchAdditional04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("]", @"[\]]"));
		}

		[TestMethod]
		public void WildMatchAdditional05()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(@"\]", @"[\]]"));
		}

		[TestMethod]
		public void WildMatchAdditional06()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(@"\", @"[\]]"));
		}

		[TestMethod]
		public void WildMatchAdditional07()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ab", "a[]b"));
		}

		[TestMethod]
		public void WildMatchAdditional08()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("a[]b", "a[]b")); // 1 1 1 1
		}

		[TestMethod]
		public void WildMatchAdditional09()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ab[", "ab[")); // 1 1 1 1
		}

		[TestMethod]
		public void WildMatchAdditional10()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ab", "[!"));
		}

		[TestMethod]
		public void WildMatchAdditional11()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("ab", "[-"));
		}

		[TestMethod]
		public void WildMatchAdditional12()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[-]"));
		}

		[TestMethod]
		public void WildMatchAdditional13()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("-", "[a-"));
		}

		[TestMethod]
		public void WildMatchAdditional14()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("-", "[!a-"));
		}

		[TestMethod]
		public void WildMatchAdditional15()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[--A]"));
		}

		[TestMethod]
		public void WildMatchAdditional16()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("5", "[--A]"));
		}

		[TestMethod]
		public void WildMatchAdditional17()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(" ", "[ --]"));
		}

		[TestMethod]
		public void WildMatchAdditional18()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("$", "[ --]"));
		}

		[TestMethod]
		public void WildMatchAdditional19()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[ --]"));
		}

		[TestMethod]
		public void WildMatchAdditional20()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("0", "[ --]"));
		}

		[TestMethod]
		public void WildMatchAdditional21()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[---]"));
		}

		[TestMethod]
		public void WildMatchAdditional22()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[------]"));
		}

		[TestMethod]
		public void WildMatchAdditional23()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("j", "[a-e-n]"));
		}

		[TestMethod]
		public void WildMatchAdditional24()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[a-e-n]"));
		}

		[TestMethod]
		public void WildMatchAdditional25()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a", "[!------]"));
		}

		[TestMethod]
		public void WildMatchAdditional26()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("[", "[]-a]"));
		}

		[TestMethod]
		public void WildMatchAdditional27()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("^", "[]-a]"));
		}

		[TestMethod]
		public void WildMatchAdditional28()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("^", "[!]-a]"));
		}

		[TestMethod]
		public void WildMatchAdditional29()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[", "[!]-a]"));
		}

		[TestMethod]
		public void WildMatchAdditional30()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("^", "[a^bc]"));
		}

		[TestMethod]
		public void WildMatchAdditional31()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-b]", "[a-]b]"));
		}

		[TestMethod]
		public void WildMatchAdditional32()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(@"\", @"[\]"));
		}

		[TestMethod]
		public void WildMatchAdditional33()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(@"\", @"[\\]"));
		}

		[TestMethod]
		public void WildMatchAdditional34()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants(@"\", @"[!\\]"));
		}

		[TestMethod]
		public void WildMatchAdditional35()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("G", @"[A-\\]"));
		}

		[TestMethod]
		public void WildMatchAdditional36()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("aaabbb", "b*a"));
		}

		[TestMethod]
		public void WildMatchAdditional37()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("aabcaa", "*ba*"));
		}

		[TestMethod]
		public void WildMatchAdditional38()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(",", "[,]"));
		}

		[TestMethod]
		public void WildMatchAdditional39()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(",", @"[\\,]"));
		}

		[TestMethod]
		public void WildMatchAdditional40()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(@"\", @"[\\,]"));
		}

		[TestMethod]
		public void WildMatchAdditional41()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-", "[,-.]"));
		}

		[TestMethod]
		public void WildMatchAdditional42()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("+", "[,-.]"));
		}

		[TestMethod]
		public void WildMatchAdditional43()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("-.]", "[,-.]"));
		}

		[TestMethod]
		public void WildMatchAdditional44()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("2", @"[\1-\3]"));
		}

		[TestMethod]
		public void WildMatchAdditional45()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("3", @"[\1-\3]"));
		}

		[TestMethod]
		public void WildMatchAdditional46()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("4", @"[\1-\3]"));
		}

		[TestMethod]
		public void WildMatchAdditional47()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants(@"\", @"[[-\]]"));
		}

		[TestMethod]
		public void WildMatchAdditional48()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("[", @"[[-\]]"));
		}

		[TestMethod]
		public void WildMatchAdditional49()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("]", @"[[-\]]"));
		}

		[TestMethod]
		public void WildMatchAdditional50()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("-", @"[[-\]]"));
		}
		#endregion

		#region Test recursion
		[TestMethod]
		public void WildMatchRecursion01()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("-adobe-courier-bold-o-normal--12-120-75-75-m-70-iso8859-1", "-*-*-*-*-*-*-12-*-*-*-m-*-*-*"));
		}

		[TestMethod]
		public void WildMatchRecursion02()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("-adobe-courier-bold-o-normal--12-120-75-75-X-70-iso8859-1", "-*-*-*-*-*-*-12-*-*-*-m-*-*-*"));
		}

		[TestMethod]
		public void WildMatchRecursion03()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("-adobe-courier-bold-o-normal--12-120-75-75-/-70-iso8859-1", "-*-*-*-*-*-*-12-*-*-*-m-*-*-*"));
		}

		[TestMethod]
		public void WildMatchRecursion04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("XXX/adobe/courier/bold/o/normal//12/120/75/75/m/70/iso8859/1", "XXX/*/*/*/*/*/*/12/*/*/*/m/*/*/*"));
		}

		[TestMethod]
		public void WildMatchRecursion05()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("XXX/adobe/courier/bold/o/normal//12/120/75/75/X/70/iso8859/1", "XXX/*/*/*/*/*/*/12/*/*/*/m/*/*/*"));
		}

		[TestMethod]
		public void WildMatchRecursion06()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("abcd/abcdefg/abcdefghijk/abcdefghijklmnop.txt", "**/*a*b*g*n*t"));
		}

		[TestMethod]
		public void WildMatchRecursion07()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("abcd/abcdefg/abcdefghijk/abcdefghijklmnop.txtz", "**/*a*b*g*n*t"));
		}

		[TestMethod]
		public void WildMatchRecursion08()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "*/*/*"));
		}

		[TestMethod]
		public void WildMatchRecursion09()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo/bar", "*/*/*"));
		}

		[TestMethod]
		public void WildMatchRecursion10()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bba/arr", "*/*/*"));
		}

		[TestMethod]
		public void WildMatchRecursion11()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bb/aa/rr", "*/*/*"));
		}

		[TestMethod]
		public void WildMatchRecursion12()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bb/aa/rr", "**/**/**"));
		}

		[TestMethod]
		public void WildMatchRecursion13()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("abcXdefXghi", "*X*i"));
		}

		[TestMethod]
		public void WildMatchRecursion14()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("ab/cXd/efXg/hi", "*X*i"));
		}

		[TestMethod]
		public void WildMatchRecursion15()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("ab/cXd/efXg/hi", "*/*X*/*/*i"));
		}

		[TestMethod]
		public void WildMatchRecursion16()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("ab/cXd/efXg/hi", "**/*X*/**/*i"));
		}
		#endregion

		#region Extra pathmatch tests
		[TestMethod]
		public void WildMatchPathmatch01()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo", "fo"));
		}

		[TestMethod]
		public void WildMatchPathmatch02()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bar", "foo/bar"));
		}

		[TestMethod]
		public void WildMatchPathmatch03()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bar", "foo/*"));
		}

		[TestMethod]
		public void WildMatchPathmatch04()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bba/arr", "foo/*"));
		}

		[TestMethod]
		public void WildMatchPathmatch05()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("foo/bba/arr", "foo/**"));
		}

		[TestMethod]
		public void WildMatchPathmatch06()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bba/arr", "foo*"));
		}

		[TestMethod]
		public void WildMatchPathmatch07()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bba/arr", "foo**")); //  1 1 1 1
		}

		[TestMethod]
		public void WildMatchPathmatch08()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bba/arr", "foo/*arr"));
		}

		[TestMethod]
		public void WildMatchPathmatch09()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bba/arr", "foo/**arr"));
		}

		[TestMethod]
		public void WildMatchPathmatch10()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo/bba/arr", "foo/*z"));
		}

		[TestMethod]
		public void WildMatchPathmatch11()
		{
			Assert.AreEqual("0 0 0 0", TestMatchVariants("foo/bba/arr", "foo/**z"));
		}

		[TestMethod]
		public void WildMatchPathmatch12()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "foo?bar"));
		}

		[TestMethod]
		public void WildMatchPathmatch13()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "foo[/]bar"));
		}

		[TestMethod]
		public void WildMatchPathmatch14()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("foo/bar", "foo[^a-z]bar"));
		}

		[TestMethod]
		public void WildMatchPathmatch15()
		{
			Assert.AreEqual("0 0 1 1", TestMatchVariants("ab/cXd/efXg/hi", "*Xg*i"));
		}
		#endregion

		#region Extra case-sensitivity tests
		[TestMethod]
		public void WildMatchCase01()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("a", "[A-Z]"));
		}

		[TestMethod]
		public void WildMatchCase02()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("A", "[A-Z]"));
		}

		[TestMethod]
		public void WildMatchCase03()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("A", "[a-z]"));
		}

		[TestMethod]
		public void WildMatchCase04()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a", "[a-z]"));
		}

		[TestMethod]
		public void WildMatchCase05()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("a", "[[:upper:]]"));
		}

		[TestMethod]
		public void WildMatchCase06()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("A", "[[:upper:]]"));
		}

		[TestMethod]
		public void WildMatchCase07()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("A", "[[:lower:]]"));
		}

		[TestMethod]
		public void WildMatchCase08()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a", "[[:lower:]]"));
		}

		[TestMethod]
		public void WildMatchCase09()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("A", "[B-Za]"));
		}

		[TestMethod]
		public void WildMatchCase10()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a", "[B-Za]"));
		}

		[TestMethod]
		public void WildMatchCase11()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("A", "[B-a]"));
		}

		[TestMethod]
		public void WildMatchCase12()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("a", "[B-a]"));
		}

		[TestMethod]
		public void WildMatchCase13()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("z", "[Z-y]"));
		}

		[TestMethod]
		public void WildMatchCase14()
		{
			Assert.AreEqual("1 1 1 1", TestMatchVariants("Z", "[Z-y]"));
		}
		#endregion

		#region Globalization tests
		[TestMethod]
		public void WildMatchGlobalization01()
		{
			Assert.AreEqual("0 1 0 1", TestMatchVariants("Äpfel", "äpfel"));
		}
		#endregion

		private static string TestMatchVariants(string text, string pattern)
		{
			string result = string.Empty;
			result += TestMatchVariant(pattern, text, WildMatch.WM_PATHNAME);
			result += " ";
			result += TestMatchVariant(pattern, text, WildMatch.WM_PATHNAME | WildMatch.WM_CASEFOLD);
			result += " ";
			result += TestMatchVariant(pattern, text, 0);
			result += " ";
			result += TestMatchVariant(pattern, text, WildMatch.WM_CASEFOLD);
			return result;
		}

		private static string TestMatchVariant(string pattern, string text, int flags)
		{
			return WildMatch.wildmatch(pattern, text, flags) switch
			{
				WildMatch.WM_MATCH => "1",
				WildMatch.WM_NOMATCH or WildMatch.WM_ABORT_ALL or WildMatch.WM_ABORT_TO_STARSTAR or WildMatch.WM_ABORT_MALFORMED => "0",
				_ => "E",
			};
		}
	}
}
