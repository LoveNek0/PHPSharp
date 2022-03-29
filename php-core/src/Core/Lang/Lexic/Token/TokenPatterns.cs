using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Lexic.Token
{
    public static class TokenPatterns
    {
        private static readonly Dictionary<TokenType, string> patterns = new Dictionary<TokenType, string>()
        {
            //  Arithmetic Operator
            {TokenType.T_ADD,                       @"+" },                         //  +
            {TokenType.T_SUB,                       @"-" },                         //  -
            {TokenType.T_MUL,                       @"*" },                         //  *
            {TokenType.T_DIV,                       @"/" },                         //  /
            {TokenType.T_MOD,                       @"%" },                         //  %
            {TokenType.T_POW,                       @"**" },                        //  **

            //  Bitwise Operator
            {TokenType.T_BIT_AND,                   @"&" },                         //  &
            {TokenType.T_BIT_XOR,                   @"^" },                         //  ^
            {TokenType.T_BIT_NOT,                   @"|" },                         //  |
            {TokenType.T_BIT_SHIFT_LEFT,            @"<<" },                        //  <<
            {TokenType.T_BIT_SHIFT_RIGHT,           @">>" },                        //  >>

            //  String operator
            {TokenType.T_CONCAT,                    @"." },                         //  .

            //  Assignment Operator
            {TokenType.T_EQUAL,                     @"=" },                         //  =

            //  Null Coalescing Operator
            {TokenType.T_COALESCE,                  @"??" },                        //  ??

            //  Assignment with Arithmetic Operator
            {TokenType.T_PLUS_EQUAL,                @"+="},                         //  +=
            {TokenType.T_MINUS_EQUAL,               @"-="},                         //  -=
            {TokenType.T_MUL_EQUAL,                 @"*="},                         //  *=
            {TokenType.T_DIV_EQUAL,                 @"/=" },                        //  /=
            {TokenType.T_MOD_EQUAL,                 @"%="},                         //  %=
            {TokenType.T_POW_EQUAL,                 @"**="},                        //  **=

            //  Assignment with Bitwise Operator
            {TokenType.T_BIT_AND_EQUAL,             @"&=" },                        //  &=
            {TokenType.T_BIT_XOR_EQUAL,             @"^=" },                        //  ^=
            {TokenType.T_BIT_NOT_EQUAL,             @"|=" },                        //  |=
            {TokenType.T_BIT_SHIFT_LEFT_EQUAL,      @"<<=" },                       //  <<=
            {TokenType.T_BIT_SHIFT_RIGHT_EQUAL,     @">>=" },                       //  >>=

            //  Assignment with String Operator
            {TokenType.T_CONCAT_EQUAL,              @".=" },                        //  .=

            //  Assignment with Null Coalescing Operator
            {TokenType.T_COALESCE_EQUAL,            @"??=" },                       //  ??=

            //  Logical Operator
            {TokenType.T_LOGICAL_AND,               @"[([&][&])([Aa][Nn][Dd])]" },  //  && and
            {TokenType.T_LOGICAL_OR,                @"[([|][|])([o][r])]" },        //  || or
            {TokenType.T_LOGICAL_XOR,               @"xor" },                       //  xor

            //  Comparison Operator
            {TokenType.T_IS_EQUAL,                  @"==" },                        //  ==
            {TokenType.T_IS_IDENTICAL,              @"===" },                       //  ===
            {TokenType.T_IS_NOT_EQUAL,              @"[([!][=])([<][>])]" },        //  != <>
            {TokenType.T_IS_NOT_IDENTICAL,          @"!==" },                       //  !==
            {TokenType.T_IS_GREATER,                @">" },                         // >
            {TokenType.T_IS_SMALLER,                @"<" },                         // <
            {TokenType.T_IS_GREATER_OR_EQUAL,       @">=" },                        //  >=
            {TokenType.T_IS_SMALLER_OR_EQUAL,       @"<=" },                        //  <=
            {TokenType.T_SPACESHIP,                 @"<=>" },                       //  <=>

            //  Incrementing/Decrementing Operator
            {TokenType.T_DEC,                       @"--" },                        //  --
            {TokenType.T_INC,                       @"++" },                        //  ++

            //  Data
            {TokenType.T_LNUMBER,                   @"[0-9]+" },                    //  123 012 0x1ac
            {TokenType.T_DNUMBER,                   @"([0-9]+)[.]([0-9]+)" },       //  
            {TokenType.T_CONSTANT_ENCAPSED_STRING,  @"TODO" },                      //  "" ''
            {TokenType.T_STRING,                    @"[a-zA-Z_][a-zA-Z0-9_]*" },    //  parent self T_CONSTANT_ENCAPSED_STRING

            //  Variable
            {TokenType.T_VARIABLE,                  @"([$]([a-zA-Z_][a-zA-Z0-9_]*))" },              //  $var_1

            //  Function
            {TokenType.T_FUNCTION,                  @"function" },                  //  function
            {TokenType.T_STATIC,                    @"static" },                    //  static
            {TokenType.T_RETURN,                    @"return" },                    //  return

            //  Class
            {TokenType.T_ABSTRACT,                  @"abstract" },				    //  abstract
            {TokenType.T_CLASS,                     @"class" },                     //  class
            {TokenType.T_PUBLIC,                    @"public" },                    //  public
            {TokenType.T_PRIVATE,                   @"private" },                   //  private
            {TokenType.T_PROTECTED,                 @"protected" },                 //  protected



            {TokenType.T_ATTRIBUTE,                 @"attribytes"},                //  attribytes
            {TokenType.T_CALLABLE,                  @"callable"},                 //  callable
            {TokenType.T_CASE,                      @"case"},                     //  case
            {TokenType.T_BREAK,                     @"break"},                    //  break
            {TokenType.T_CATCH,                     @"catch"},                    //  catch
            {TokenType.T_CLONE,                     @"clone"},                    //  clone
            {TokenType.T_CONST,                     @"const"},                    //  const
            {TokenType.T_CONTINUE,                  @"continue"},                 //  continue
            {TokenType.T_DECLARE,                   @"declare"},                  //  declare
            {TokenType.T_DEFAULT,                   @"default"},                  //  default

            {TokenType.T_AS,                        @"as"},                       //  as
            
            {TokenType.T_ARRAY,                     @"array"},                    //  array()
            {TokenType.T_ARRAY_BRACE_OPEN,          @"\["},
            {TokenType.T_ARRAY_BRACE_CLOSE,         @"\]" },

            //  Casting
            {TokenType.T_ARRAY_CAST,                @"(array)"},               //  (array)
            {TokenType.T_BOOL_CAST,                 @"\(bool\)|\(boolean\)"},                //  (bool) (boolean)
            
            //  Assignment with arithmetic operation
            
            {TokenType.T_PAAMAYIM_NEKUDOTAYIM, "TODO"},     //  ::
            //  
            

            {TokenType.T_CURLY_OPEN,                @"TODO"},               //  {$

            {TokenType.T_OPEN_TAG,                  @"[<](([?]([pP][hH][pP])?)|[%])"},
            {TokenType.T_CLOSE_TAG,                 @"[?%][>]"},                //  ?> %>

            {TokenType.T_CLASS_C,                   @"__[Cc][Ll][Aa][Ss][Ss]__"},                  //  __CLASS__
            {TokenType.T_DIR,                       @"__[Dd][Ii][Rr]__"},                      //  __DIR__
            
            {TokenType.T_DOC_COMMENT,               @"\/\*\*[\s\S]*\*\/"},                  // /** */
            {TokenType.T_COMMENT,                   @"(((\/\/)|#).*)|(\/\*[\s\S]*\*\/)"},   // // # /* */

            {TokenType.T_BAD_CHARACTER,             @"TODO"},            //  0x00-0x1F without \t (0x09), "TODO"}, \n (0x0a) и \r (0x0d)
            {TokenType.T_DO, "TODO"},                       //  do=
            {TokenType.T_DOLLAR_OPEN_CURLY_BRACES, "TODO"}, //  ${
            {TokenType.T_DOUBLE_ARROW, "TODO"},             //  =>
            {TokenType.T_DOUBLE_CAST, "TODO"},              //  (real) (double) (float)
            {TokenType.T_DOUBLE_COLON, "TODO"},             //  ::
            {TokenType.T_ECHO, "TODO"},                     //  echo
            {TokenType.T_ELLIPSIS, "TODO"},                 //  ...
            {TokenType.T_ELSE, "TODO"},                     //  else
            {TokenType.T_ELSEIF, "TODO"},                   //  elseif
            {TokenType.T_EMPTY, "TODO"},                    //  empty()
            {TokenType.T_ENCAPSED_AND_WHITESPACE, "TODO"},  //  "$var"
            {TokenType.T_ENDDECLARE, "TODO"},               //  enddeclare
            {TokenType.T_ENDFOR, "TODO"},                   //  endfor
            {TokenType.T_ENDFOREACH, "TODO"},               //  endforeach
            {TokenType.T_ENDIF, "TODO"},                    //  endif
            {TokenType.T_ENDSWITCH, "TODO"},                //  endswitch
            {TokenType.T_ENDWHILE, "TODO"},                 //  endwhile
            {TokenType.T_END_HEREDOC, "TODO"},              //  END
            {TokenType.T_EVAL, "TODO"},                     //  eval()
            {TokenType.T_EXIT, "TODO"},                     //  exit() die()
            {TokenType.T_EXTENDS, "TODO"},                  //  extends
            {TokenType.T_FILE, "TODO"},                     //  __FILE__
            {TokenType.T_FINAL, "TODO"},                    //  final
            {TokenType.T_FINALLY, "TODO"},                  //  finally
            {TokenType.T_FN, "TODO"},                       //  fn
            {TokenType.T_FOR, "TODO"},                      //  for
            {TokenType.T_FOREACH, "TODO"},                  //  foreach
            {TokenType.T_FUNC_C, "TODO"},                   //  __FUNCTION__
            {TokenType.T_GLOBAL, "TODO"},                   //  global
            {TokenType.T_GOTO, "TODO"},                     //  goto
            {TokenType.T_HALT_COMPILER, "TODO"},            //  __halt_compiler()
            {TokenType.T_IF, "TODO"},                       //  if
            {TokenType.T_IMPLEMENTS, "TODO"},               //  implements
            {TokenType.T_INC, "TODO"},                      //  ++
            {TokenType.T_INCLUDE, "TODO"},                  //  include
            {TokenType.T_INCLUDE_ONCE, "TODO"},             //  include_once
            {TokenType.T_INLINE_HTML, "TODO"},              //  any text outside php code tags
            {TokenType.T_INSTANCEOF, "TODO"},               //  instanceof
            {TokenType.T_INSTEADOF, "TODO"},                //  insteadof
            {TokenType.T_INTERFACE, "TODO"},                //  interface
            {TokenType.T_INT_CAST, "TODO"},                 //  (int) (integer)
            {TokenType.T_ISSET, "TODO"},                    //  isset()
            {TokenType.T_LINE, "TODO"},                     //  __LINE__
            {TokenType.T_LIST, "TODO"},                     //  list()
            {TokenType.T_MATCH, "TODO"},                    //  match
            {TokenType.T_METHOD_C, "TODO"},                 //  __METHOD__
            {TokenType.T_NAMESPACE, "TODO"},                //  namespace
            {TokenType.T_NAME_FULLY_QUALIFIED, "TODO"},     //  \App\Namespace
            {TokenType.T_NEW, "TODO"},                      //  new
            {TokenType.T_NS_C, "TODO"},                     //  __NAMESPACE__
            {TokenType.T_NS_SEPARATOR, "TODO"},             //  \
            {TokenType.T_NUM_STRING, "TODO"},               //  "$var[0]"
            {TokenType.T_OBJECT_CAST, "TODO"},              //  (object)
            {TokenType.T_OBJECT_OPERATOR, "TODO"},          //  ->
            {TokenType.T_NULLSAFE_OBJECT_OPERATOR, "TODO"}, //  ?->
            {TokenType.T_OPEN_TAG, "TODO"},                 //  <?php <? <%
            {TokenType.T_OPEN_TAG_WITH_ECHO, "TODO"},       //  <?= <%=
            {TokenType.T_PRINT, "TODO"},                    //  print()
            {TokenType.T_REQUIRE, "TODO"},                  //  require
            {TokenType.T_REQUIRE_ONCE, "TODO"},             //  require_once
            {TokenType.T_START_HEREDOC, "TODO"},            //  <<<
            {TokenType.T_STRING_CAST, "TODO"},              //  (string)
            {TokenType.T_STRING_VARNAME, "TODO"},           //  "${a
            {TokenType.T_SWITCH, "TODO"},                   //  switch
            {TokenType.T_THROW, "TODO"},                    //  throw
            {TokenType.T_TRAIT, "TODO"},                    //  trait
            {TokenType.T_TRAIT_C, "TODO"},                  //  __TRAIT__
            {TokenType.T_TRY, "TODO"},                      //  try
            {TokenType.T_UNSET, "TODO"},                    //  unset()
            {TokenType.T_UNSET_CAST, "TODO"},               //  (unset)
            {TokenType.T_USE, "TODO"},                      //  use
            {TokenType.T_VAR, "TODO"},                      //  var
            {TokenType.T_VARIABLE, "TODO"},                 //  $var
            {TokenType.T_WHILE, "TODO"},                    //  while
            {TokenType.T_WHITESPACE, "TODO"},               //  \t \r \n
            {TokenType.T_YIELD, "TODO"},                    //  yield
            {TokenType.T_YIELD_FROM, "TODO"},               //  yield_from

            
            {TokenType.T_COMMA, "[,]"},

            {TokenType.T_SEMICOLON, "[;]"},
            {TokenType.T_WHITESPACE, @"[\n\t\r\s][\n\t\r\s]*"},

            {TokenType.T_BRACE_OPEN, "[(]"},
            {TokenType.T_BRACE_CLOSE, "[)]"},
            {TokenType.T_CURLY_BRACE_OPEN, "[{]"},
            {TokenType.T_CURLY_BRACE_CLOSE, "[}]"}
        };

        public static Regex GetPatternRegex(this TokenType type)
        {
            return new Regex(GetPatternString(type));
        }

        public static string GetPatternString(this TokenType type)
        {
            return "^(" + patterns[type] + ")";
        }

        public static TokenType[] GetTypes()
        {
            return patterns.Keys.ToArray<TokenType>();
        }
    }
}
