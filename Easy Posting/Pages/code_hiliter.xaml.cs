using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Net;
using System.Drawing;
using System.Collections;
using System.ComponentModel;

namespace Easy_Posting.Content
{
    /// <summary>
    /// Interaction logic for code_hiliter.xaml
    /// </summary>
    public partial class code_hiliter : UserControl
    {
        App thisApp = App.Current as App;
        byte[] responsebytes;

        public code_hiliter()
        {
            InitializeComponent();
        }

        public class ListSourceClass
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public class LexerList : List<ListSourceClass>
        {
            public LexerList()
            {
                XmlDocument doc = new XmlDocument();

                #region  언어 종류
                doc.LoadXml(@"<select name=""lexer"">
              
              <option value=""abap"">ABAP</option>
              
              <option value=""as"">ActionScript</option>
              
              <option value=""as3"">ActionScript 3</option>
              
              <option value=""ada"">Ada</option>
              
              <option value=""antlr"">ANTLR</option>
              
              <option value=""antlr-as"">ANTLR With ActionScript Target</option>
              
              <option value=""antlr-csharp"">ANTLR With C# Target</option>
              
              <option value=""antlr-cpp"">ANTLR With CPP Target</option>
              
              <option value=""antlr-java"">ANTLR With Java Target</option>
              
              <option value=""antlr-objc"">ANTLR With ObjectiveC Target</option>
              
              <option value=""antlr-perl"">ANTLR With Perl Target</option>
              
              <option value=""antlr-python"">ANTLR With Python Target</option>
              
              <option value=""antlr-ruby"">ANTLR With Ruby Target</option>
              
              <option value=""apacheconf"">ApacheConf</option>
              
              <option value=""applescript"">AppleScript</option>
              
              <option value=""aspectj"">AspectJ</option>
              
              <option value=""aspx-cs"">aspx-cs</option>
              
              <option value=""aspx-vb"">aspx-vb</option>
              
              <option value=""asy"">Asymptote</option>
              
              <option value=""ahk"">autohotkey</option>
              
              <option value=""autoit"">AutoIt</option>
              
              <option value=""awk"">Awk</option>
              
              <option value=""basemake"">Base Makefile</option>
              
              <option value=""bash"">Bash</option>
              
              <option value=""console"">Bash Session</option>
              
              <option value=""bat"">Batchfile</option>
              
              <option value=""bbcode"">BBCode</option>
              
              <option value=""befunge"">Befunge</option>
              
              <option value=""blitzmax"">BlitzMax</option>
              
              <option value=""boo"">Boo</option>
              
              <option value=""brainfuck"">Brainfuck</option>
              
              <option value=""bro"">Bro</option>
              
              <option value=""bugs"">BUGS</option>
              
              <option value=""c"">C</option>
              
              <option selected=""selected"" value=""csharp"">C#</option>
              
              <option value=""cpp"">C++</option>
              
              <option value=""c-objdump"">c-objdump</option>
              
              <option value=""ca65"">ca65</option>
              
              <option value=""cbmbas"">CBM BASIC V2</option>
              
              <option value=""ceylon"">Ceylon</option>
              
              <option value=""cfengine3"">CFEngine3</option>
              
              <option value=""cfs"">cfstatement</option>
              
              <option value=""cheetah"">Cheetah</option>
              
              <option value=""clojure"">Clojure</option>
              
              <option value=""cmake"">CMake</option>
              
              <option value=""cobol"">COBOL</option>
              
              <option value=""cobolfree"">COBOLFree</option>
              
              <option value=""coffee-script"">CoffeeScript</option>
              
              <option value=""cfm"">Coldfusion HTML</option>
              
              <option value=""common-lisp"">Common Lisp</option>
              
              <option value=""coq"">Coq</option>
              
              <option value=""cpp-objdump"">cpp-objdump</option>
              
              <option value=""croc"">Croc</option>
              
              <option value=""css"">CSS</option>
              
              <option value=""css+django"">CSS+Django/Jinja</option>
              
              <option value=""css+genshitext"">CSS+Genshi Text</option>
              
              <option value=""css+lasso"">CSS+Lasso</option>
              
              <option value=""css+mako"">CSS+Mako</option>
              
              <option value=""css+myghty"">CSS+Myghty</option>
              
              <option value=""css+php"">CSS+PHP</option>
              
              <option value=""css+erb"">CSS+Ruby</option>
              
              <option value=""css+smarty"">CSS+Smarty</option>
              
              <option value=""cuda"">CUDA</option>
              
              <option value=""cython"">Cython</option>
              
              <option value=""d"">D</option>
              
              <option value=""d-objdump"">d-objdump</option>
              
              <option value=""dpatch"">Darcs Patch</option>
              
              <option value=""dart"">Dart</option>
              
              <option value=""control"">Debian Control file</option>
              
              <option value=""sourceslist"">Debian Sourcelist</option>
              
              <option value=""delphi"">Delphi</option>
              
              <option value=""dg"">dg</option>
              
              <option value=""diff"">Diff</option>
              
              <option value=""django"">Django/Jinja</option>
              
              <option value=""dtd"">DTD</option>
              
              <option value=""duel"">Duel</option>
              
              <option value=""dylan"">Dylan</option>
              
              <option value=""dylan-console"">Dylan session</option>
              
              <option value=""dylan-lid"">DylanLID</option>
              
              <option value=""ec"">eC</option>
              
              <option value=""ecl"">ECL</option>
              
              <option value=""elixir"">Elixir</option>
              
              <option value=""iex"">Elixir iex session</option>
              
              <option value=""ragel-em"">Embedded Ragel</option>
              
              <option value=""erb"">ERB</option>
              
              <option value=""erlang"">Erlang</option>
              
              <option value=""erl"">Erlang erl session</option>
              
              <option value=""evoque"">Evoque</option>
              
              <option value=""factor"">Factor</option>
              
              <option value=""fancy"">Fancy</option>
              
              <option value=""fan"">Fantom</option>
              
              <option value=""felix"">Felix</option>
              
              <option value=""fortran"">Fortran</option>
              
              <option value=""Clipper"">FoxPro</option>
              
              <option value=""fsharp"">FSharp</option>
              
              <option value=""gas"">GAS</option>
              
              <option value=""genshi"">Genshi</option>
              
              <option value=""genshitext"">Genshi Text</option>
              
              <option value=""pot"">Gettext Catalog</option>
              
              <option value=""Cucumber"">Gherkin</option>
              
              <option value=""glsl"">GLSL</option>
              
              <option value=""gnuplot"">Gnuplot</option>
              
              <option value=""go"">Go</option>
              
              <option value=""gooddata-cl"">GoodData-CL</option>
              
              <option value=""gosu"">Gosu</option>
              
              <option value=""gst"">Gosu Template</option>
              
              <option value=""groff"">Groff</option>
              
              <option value=""groovy"">Groovy</option>
              
              <option value=""haml"">Haml</option>
              
              <option value=""haskell"">Haskell</option>
              
              <option value=""hx"">haXe</option>
              
              <option value=""html"">HTML</option>
              
              <option value=""html+cheetah"">HTML+Cheetah</option>
              
              <option value=""html+django"">HTML+Django/Jinja</option>
              
              <option value=""html+evoque"">HTML+Evoque</option>
              
              <option value=""html+genshi"">HTML+Genshi</option>
              
              <option value=""html+lasso"">HTML+Lasso</option>
              
              <option value=""html+mako"">HTML+Mako</option>
              
              <option value=""html+myghty"">HTML+Myghty</option>
              
              <option value=""html+php"">HTML+PHP</option>
              
              <option value=""html+smarty"">HTML+Smarty</option>
              
              <option value=""html+velocity"">HTML+Velocity</option>
              
              <option value=""http"">HTTP</option>
              
              <option value=""haxeml"">Hxml</option>
              
              <option value=""hybris"">Hybris</option>
              
              <option value=""idl"">IDL</option>
              
              <option value=""ini"">INI</option>
              
              <option value=""io"">Io</option>
              
              <option value=""ioke"">Ioke</option>
              
              <option value=""irc"">IRC logs</option>
              
              <option value=""jade"">Jade</option>
              
              <option value=""jags"">JAGS</option>
              
              <option value=""java"">Java</option>
              
              <option value=""jsp"">Java Server Page</option>
              
              <option value=""js"">JavaScript</option>
              
              <option value=""js+cheetah"">JavaScript+Cheetah</option>
              
              <option value=""js+django"">JavaScript+Django/Jinja</option>
              
              <option value=""js+genshitext"">JavaScript+Genshi Text</option>
              
              <option value=""js+lasso"">JavaScript+Lasso</option>
              
              <option value=""js+mako"">JavaScript+Mako</option>
              
              <option value=""js+myghty"">JavaScript+Myghty</option>
              
              <option value=""js+php"">JavaScript+PHP</option>
              
              <option value=""js+erb"">JavaScript+Ruby</option>
              
              <option value=""js+smarty"">JavaScript+Smarty</option>
              
              <option value=""json"">JSON</option>
              
              <option value=""julia"">Julia</option>
              
              <option value=""jlcon"">Julia console</option>
              
              <option value=""kconfig"">Kconfig</option>
              
              <option value=""koka"">Koka</option>
              
              <option value=""kotlin"">Kotlin</option>
              
              <option value=""lasso"">Lasso</option>
              
              <option value=""lighty"">Lighttpd configuration file</option>
              
              <option value=""lhs"">Literate Haskell</option>
              
              <option value=""live-script"">LiveScript</option>
              
              <option value=""llvm"">LLVM</option>
              
              <option value=""logos"">Logos</option>
              
              <option value=""logtalk"">Logtalk</option>
              
              <option value=""lua"">Lua</option>
              
              <option value=""make"">Makefile</option>
              
              <option value=""mako"">Mako</option>
              
              <option value=""maql"">MAQL</option>
              
              <option value=""mason"">Mason</option>
              
              <option value=""matlab"">Matlab</option>
              
              <option value=""matlabsession"">Matlab session</option>
              
              <option value=""minid"">MiniD</option>
              
              <option value=""modelica"">Modelica</option>
              
              <option value=""modula2"">Modula-2</option>
              
              <option value=""trac-wiki"">MoinMoin/Trac Wiki markup</option>
              
              <option value=""monkey"">Monkey</option>
              
              <option value=""moocode"">MOOCode</option>
              
              <option value=""moon"">MoonScript</option>
              
              <option value=""mscgen"">Mscgen</option>
              
              <option value=""mupad"">MuPAD</option>
              
              <option value=""mxml"">MXML</option>
              
              <option value=""myghty"">Myghty</option>
              
              <option value=""mysql"">MySQL</option>
              
              <option value=""nasm"">NASM</option>
              
              <option value=""nemerle"">Nemerle</option>
              
              <option value=""newlisp"">NewLisp</option>
              
              <option value=""newspeak"">Newspeak</option>
              
              <option value=""nginx"">Nginx configuration file</option>
              
              <option value=""nimrod"">Nimrod</option>
              
              <option value=""nsis"">NSIS</option>
              
              <option value=""numpy"">NumPy</option>
              
              <option value=""objdump"">objdump</option>
              
              <option value=""objective-c"">Objective-C</option>
              
              <option value=""objective-c++"">Objective-C++</option>
              
              <option value=""objective-j"">Objective-J</option>
              
              <option value=""ocaml"">OCaml</option>
              
              <option value=""octave"">Octave</option>
              
              <option value=""ooc"">Ooc</option>
              
              <option value=""opa"">Opa</option>
              
              <option value=""openedge"">OpenEdge ABL</option>
              
              <option value=""perl"">Perl</option>
              
              <option value=""php"">PHP</option>
              
              <option value=""plpgsql"">PL/pgSQL</option>
              
              <option value=""psql"">PostgreSQL console (psql)</option>
              
              <option value=""postgresql"">PostgreSQL SQL dialect</option>
              
              <option value=""postscript"">PostScript</option>
              
              <option value=""pov"">POVRay</option>
              
              <option value=""powershell"">PowerShell</option>
              
              <option value=""prolog"">Prolog</option>
              
              <option value=""properties"">Properties</option>
              
              <option value=""protobuf"">Protocol Buffer</option>
              
              <option value=""puppet"">Puppet</option>
              
              <option value=""pypylog"">PyPy Log</option>
              
              <option value=""python"">Python</option>
              
              <option value=""python3"">Python 3</option>
              
              <option value=""py3tb"">Python 3.0 Traceback</option>
              
              <option value=""pycon"">Python console session</option>
              
              <option value=""pytb"">Python Traceback</option>
              
              <option value=""qml"">QML</option>
              
              <option value=""racket"">Racket</option>
              
              <option value=""ragel"">Ragel</option>
              
              <option value=""ragel-c"">Ragel in C Host</option>
              
              <option value=""ragel-cpp"">Ragel in CPP Host</option>
              
              <option value=""ragel-d"">Ragel in D Host</option>
              
              <option value=""ragel-java"">Ragel in Java Host</option>
              
              <option value=""ragel-objc"">Ragel in Objective C Host</option>
              
              <option value=""ragel-ruby"">Ragel in Ruby Host</option>
              
              <option value=""raw"">Raw token data</option>
              
              <option value=""rconsole"">RConsole</option>
              
              <option value=""rd"">Rd</option>
              
              <option value=""rebol"">REBOL</option>
              
              <option value=""redcode"">Redcode</option>
              
              <option value=""registry"">reg</option>
              
              <option value=""rst"">reStructuredText</option>
              
              <option value=""rhtml"">RHTML</option>
              
              <option value=""RobotFramework"">RobotFramework</option>
              
              <option value=""spec"">RPMSpec</option>
              
              <option value=""rb"">Ruby</option>
              
              <option value=""rbcon"">Ruby irb session</option>
              
              <option value=""rust"">Rust</option>
              
              <option value=""splus"">S</option>
              
              <option value=""sass"">Sass</option>
              
              <option value=""scala"">Scala</option>
              
              <option value=""ssp"">Scalate Server Page</option>
              
              <option value=""scaml"">Scaml</option>
              
              <option value=""scheme"">Scheme</option>
              
              <option value=""scilab"">Scilab</option>
              
              <option value=""scss"">SCSS</option>
              
              <option value=""shell-session"">Shell Session</option>
              
              <option value=""smali"">Smali</option>
              
              <option value=""smalltalk"">Smalltalk</option>
              
              <option value=""smarty"">Smarty</option>
              
              <option value=""snobol"">Snobol</option>
              
              <option value=""sp"">SourcePawn</option>
              
              <option value=""sql"">SQL</option>
              
              <option value=""sqlite3"">sqlite3con</option>
              
              <option value=""squidconf"">SquidConf</option>
              
              <option value=""stan"">Stan</option>
              
              <option value=""sml"">Standard ML</option>
              
              <option value=""systemverilog"">systemverilog</option>
              
              <option value=""tcl"">Tcl</option>
              
              <option value=""tcsh"">Tcsh</option>
              
              <option value=""tea"">Tea</option>
              
              <option value=""tex"">TeX</option>
              
              <option value=""text"">Text only</option>
              
              <option value=""treetop"">Treetop</option>
              
              <option value=""ts"">TypeScript</option>
              
              <option value=""urbiscript"">UrbiScript</option>
              
              <option value=""vala"">Vala</option>
              
              <option value=""vb.net"">VB.net</option>
              
              <option value=""velocity"">Velocity</option>
              
              <option value=""verilog"">verilog</option>
              
              <option value=""vgl"">VGL</option>
              
              <option value=""vhdl"">vhdl</option>
              
              <option value=""vim"">VimL</option>
              
              <option value=""xml"">XML</option>
              
              <option value=""xml+cheetah"">XML+Cheetah</option>
              
              <option value=""xml+django"">XML+Django/Jinja</option>
              
              <option value=""xml+evoque"">XML+Evoque</option>
              
              <option value=""xml+lasso"">XML+Lasso</option>
              
              <option value=""xml+mako"">XML+Mako</option>
              
              <option value=""xml+myghty"">XML+Myghty</option>
              
              <option value=""xml+php"">XML+PHP</option>
              
              <option value=""xml+erb"">XML+Ruby</option>
              
              <option value=""xml+smarty"">XML+Smarty</option>
              
              <option value=""xml+velocity"">XML+Velocity</option>
              
              <option value=""xquery"">XQuery</option>
              
              <option value=""xslt"">XSLT</option>
              
              <option value=""xtend"">Xtend</option>
              
              <option value=""yaml"">YAML</option>
              
            </select>");
                #endregion

                foreach (XmlElement elmt in doc.DocumentElement.ChildNodes)
                {
                    this.Add(new ListSourceClass() { Key = elmt.Attributes["value"].Value, Value = elmt.InnerText });
                }
                doc = null;
            }

        }

        public class StyleList : List<ListSourceClass>
        {
            public StyleList()
            {
                XmlDocument doc = new XmlDocument();

                #region  언어 종류
                doc.LoadXml(@"<select name=""style"">
              
              <option value=""autumn"">autumn</option>
              
              <option value=""borland"">borland</option>
              
              <option value=""bw"">bw</option>
              
              <option selected=""selected"" value=""colorful"">colorful</option>
              
              <option value=""default"">default</option>
              
              <option value=""emacs"">emacs</option>
              
              <option value=""friendly"">friendly</option>
              
              <option value=""fruity"">fruity</option>
              
              <option value=""manni"">manni</option>
              
              <option value=""monokai"">monokai</option>
              
              <option value=""murphy"">murphy</option>
              
              <option value=""native"">native</option>
              
              <option value=""pastie"">pastie</option>
              
              <option value=""perldoc"">perldoc</option>
              
              <option value=""rrt"">rrt</option>
              
              <option value=""tango"">tango</option>
              
              <option value=""trac"">trac</option>
              
              <option value=""vim"">vim</option>
              
              <option value=""vs"">vs</option>
              
            </select>");
                #endregion

                foreach (XmlElement elmt in doc.DocumentElement.ChildNodes)
                {
                    this.Add(new ListSourceClass() { Key = elmt.Attributes["value"].Value, Value = elmt.InnerText });
                }
                doc = null;
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int i = 0;
            LexerList lgsource = new LexerList();
            StyleList stsource = new StyleList();
            while (i!=282)
            {
                lg_list.Items.Add(lgsource[i].Value);                
                i++;
            }
            i = 0;
            while(i!=19)
            {
                style_list.Items.Add(stsource[i].Value);
                i++;
            }
            i=0;

            lg_list.SelectedIndex = 34;
            style_list.SelectedIndex = 4;
        }

        private void result_button_Click(object sender, RoutedEventArgs e)
        {
            string code = src_code.Text;
            string lexer = "";
            string style = "";
            string divstyles = ("border:solid gray;border-width:.1em .1em .1em .8em;padding:.2em .6em;");
            LexerList lgsource = new LexerList();
            StyleList stsource = new StyleList();

            int i = 0;
            while (i != 282)
            {
                if (lg_list.SelectedValue.ToString() == lgsource[i].Value)
                    lexer = lgsource[i].Key;
                i++;
            }
            i = 0;
            while (i != 19)
            {
                if (style_list.SelectedValue.ToString() == stsource[i].Value)
                    style = stsource[i].Key;
                i++;
            }
            i = 0;

            if (code == "")
            {
                ModernDialog.ShowMessage("변환할 내용이 없습니다.", "알림", MessageBoxButton.OK);
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                    reqparm.Add("code", code);
                    reqparm.Add("lexer", lexer);
                    reqparm.Add("style", style);
                    reqparm.Add("divstyles", divstyles);
                    responsebytes = wc.UploadValues("http://hilite.me/api", "POST", reqparm);
                    high.DocumentText = Encoding.UTF8.GetString(responsebytes);
                }
            }
        }

        private void insert_button_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = (Window)this.Parent;
            if (responsebytes != null)
            {
                thisApp.coderesult = Encoding.UTF8.GetString(responsebytes);
                parentWindow.Close();
            }
            else
                ModernDialog.ShowMessage("입력할 내용이 없습니다.", "알림", MessageBoxButton.OK);            
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            high.Dispose();
        }
    }
}
