using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PHP.Core.Lang.Token
{
    public static class TokenPatterns
    {
        private static readonly Dictionary<TokenType, string> patterns = new Dictionary<TokenType, string>()
            {
            //  Arithmetic Operator
            {TokenType.T_ADD,                       @"\+" },                                        //  +
            {TokenType.T_SUB,                       @"-" },                                         //  -
            {TokenType.T_MUL,                       @"\*" },                                         //  *
            {TokenType.T_DIV,                       @"/" },                                         //  /
            {TokenType.T_MOD,                       @"%" },                                         //  %
            {TokenType.T_POW,                       @"\*\*" },                                        //  **

            //  Bitwise Operator
            {TokenType.T_BIT_AND,                   @"\&" },                                         //  &
            {TokenType.T_BIT_XOR,                   @"\^" },                                         //  ^
            {TokenType.T_BIT_NOT,                   @"\|" },                                         //  |
            {TokenType.T_BIT_SHIFT_LEFT,            @"<<" },                                        //  <<
            {TokenType.T_BIT_SHIFT_RIGHT,           @">>" },                                        //  >>

            //  String operator
            {TokenType.T_CONCAT,                    @"\." },                                         //  .

            //  Assignment Operator
            {TokenType.T_EQUAL,                     @"=" },                                         //  =

            //  Null Coalescing Operator
            {TokenType.T_COALESCE,                  @"\?\?" },                                        //  ??

            //  Assignment with Arithmetic Operator
            {TokenType.T_PLUS_EQUAL,                @"\+="},                                         //  +=
            {TokenType.T_MINUS_EQUAL,               @"-="},                                         //  -=
            {TokenType.T_MUL_EQUAL,                 @"\*="},                                         //  *=
            {TokenType.T_DIV_EQUAL,                 @"/=" },                                        //  /=
            {TokenType.T_MOD_EQUAL,                 @"%="},                                         //  %=
            {TokenType.T_POW_EQUAL,                 @"\*\*="},                                        //  **=

            //  Assignment with Bitwise Operator
            {TokenType.T_BIT_AND_EQUAL,             @"\&=" },                                        //  &=
            {TokenType.T_BIT_XOR_EQUAL,             @"\^=" },                                        //  ^=
            {TokenType.T_BIT_NOT_EQUAL,             @"\|=" },                                        //  |=
            {TokenType.T_BIT_SHIFT_LEFT_EQUAL,      @"<<=" },                                       //  <<=
            {TokenType.T_BIT_SHIFT_RIGHT_EQUAL,     @">>=" },                                       //  >>=

            //  Assignment with String Operator
            {TokenType.T_CONCAT_EQUAL,              @"\.=" },                                        //  .=

            //  Assignment with Null Coalescing Operator
            {TokenType.T_COALESCE_EQUAL,            @"\?\?=" },                                       //  ??=

            //  Logical Operator
            {TokenType.T_LOGICAL_AND,               @"\&\&|[Aa][Nn][Dd]]" },                  //  && and
            {TokenType.T_LOGICAL_OR,                @"\|\||or" },                        //  || or
            {TokenType.T_LOGICAL_XOR,               @"xor" },                                       //  xor

            //  Comparison Operator
            {TokenType.T_IS_EQUAL,                  @"==" },                                        //  ==
            {TokenType.T_IS_IDENTICAL,              @"===" },                                       //  ===
            {TokenType.T_IS_NOT_EQUAL,              @"\!=|\<\>" },                        //  != <>
            {TokenType.T_IS_NOT_IDENTICAL,          @"\!==" },                                       //  !==
            {TokenType.T_IS_GREATER,                @">" },                                         // >
            {TokenType.T_IS_SMALLER,                @"<" },                                         // <
            {TokenType.T_IS_GREATER_OR_EQUAL,       @">=" },                                        //  >=
            {TokenType.T_IS_SMALLER_OR_EQUAL,       @"<=" },                                        //  <=
            {TokenType.T_SPACESHIP,                 @"<=>" },                                       //  <=>

            //  Incrementing/Decrementing Operator
            {TokenType.T_DEC,                       @"--" },                                        //  --
            {TokenType.T_INC,                       @"\+\+" },                                      //  ++

            //  Data
            {TokenType.T_LNUMBER,                   @"[0-9]+" },                                    //  123 012 0x1ac
            {TokenType.T_DNUMBER,                   @"([0-9]+)[.]([0-9]+)" },                       //  
            {TokenType.T_CONSTANT_ENCAPSED_STRING,  "\\\"([\\S\\s]*?)\\\"|\\'([\\S\\s]*?)\\'" },    //  "" ''
            {TokenType.T_STRING,                    @"[a-zA-Z_][a-zA-Z0-9_]*" },                    //  parent self T_CONSTANT_ENCAPSED_STRING

            //  Variable
            {TokenType.T_VARIABLE,                  @"([$]+([a-zA-Z_][a-zA-Z0-9_]*))" },            //  $var_1
            {TokenType.T_DOLLAR_OPEN_CURLY_BRACES,  @"${" },                                        //  ${

            //  Function
            {TokenType.T_FUNCTION,                  @"function" },                                  //  function
            {TokenType.T_STATIC,                    @"static" },                                    //  static
            {TokenType.T_RETURN,                    @"return" },                                    //  return
            {TokenType.T_YIELD,                     @"yield" },                                     //  yield
            {TokenType.T_YIELD_FROM,                @"yield from" },                                //  yield_from
            {TokenType.T_CALLABLE,                  @"callable" },                                  //  callable
            {TokenType.T_ELLIPSIS,                  @"\.\.\." },                                    //  ...
            {TokenType.T_FN,                        @"fn" },                                        //  fn

            //  Class
            {TokenType.T_ABSTRACT,                  @"abstract" },				                    //  abstract
            {TokenType.T_INTERFACE,                 @"interface" },                                 //  interface
            {TokenType.T_TRAIT,                     @"trait" },                                     //  trait
            {TokenType.T_CLASS,                     @"class" },                                     //  class
            {TokenType.T_PUBLIC,                    @"public" },                                    //  public
            {TokenType.T_PRIVATE,                   @"private" },                                   //  private
            {TokenType.T_PROTECTED,                 @"protected" },                                 //  protected
            {TokenType.T_DOUBLE_COLON,              @"::" },                                        //  ::
            {TokenType.T_OBJECT_OPERATOR,           @"->" },                                        //  ->
            {TokenType.T_NULLSAFE_OBJECT_OPERATOR,  @"[?]->" },                                     //  ?->
            {TokenType.T_CLONE,                     @"clone" },                                     //  clone
            {TokenType.T_EXTENDS,                   @"extends" },                                   //  extends
            {TokenType.T_IMPLEMENTS,                @"implements" },                                //  implements
            {TokenType.T_FINAL,                     @"final" },                                     //  final
            {TokenType.T_INSTANCEOF,                @"instanceof" },                                //  instanceof
            {TokenType.T_INSTEADOF,                 @"insteadof" },                                 //  insteadof
            {TokenType.T_NEW,                       @"new" },                                       //  new
            //  {TokenType.T_ATTRIBUTE,                 @"attribytes" },                                //  attribytes

            //  Array
            {TokenType.T_ARRAY,                     @"array" },                                     //  array()
            {TokenType.T_DOUBLE_ARROW,              @"=>" },                                        //  =>

            //  Casting
            {TokenType.T_INT_CAST,                  @"\(int\)|\(integer\)" },                       //  (int) (integer)
            {TokenType.T_DOUBLE_CAST,               @"\(real\)|\(double\)|\(float\)" },             //  (real) (double) (float)
            {TokenType.T_STRING_CAST,               @"\(string\)" },                                //  (string)
            {TokenType.T_BOOL_CAST,                 @"\(bool\)|\(boolean\)" },                      //  (bool) (boolean)
            {TokenType.T_ARRAY_CAST,                @"\(array\)" },                                 //  (array)
            {TokenType.T_OBJECT_CAST,               @"\(object\)" },                                //  (object)
            {TokenType.T_UNSET_CAST,                @"\(unset\)" },                                 //  (unset)

            //  Cycles
            {TokenType.T_DO,                        @"do" },                                        //  do
            {TokenType.T_WHILE,                     @"while" },                                     //  while
            {TokenType.T_ENDWHILE,                  @"endwhile" },                                  //  endwhile
            {TokenType.T_FOR,                       @"for" },                                       //  for
            {TokenType.T_ENDFOR,                    @"endfor" },                                    //  endfor
            {TokenType.T_FOREACH,                   @"foreach" },                                   //  foreach
            {TokenType.T_ENDFOREACH,                @"endforeach" },                                //  endforeach
            {TokenType.T_AS,                        @"as" },                                        //  as
            {TokenType.T_CONTINUE,                  @"continue" },                                  //  continue

            //  Switch
            {TokenType.T_SWITCH,                    @"switch" },                                    //  switch
            {TokenType.T_ENDSWITCH,                 @"endswitch" },                                 //  endswitch
            {TokenType.T_CASE,                      @"case" },                                      //  case
            {TokenType.T_BREAK,                     @"break" },                                     //  break
            {TokenType.T_DEFAULT,                   @"default"},                                    //  default

            //  if ... else
            {TokenType.T_IF,                        @"if" },                                        //  if
            {TokenType.T_ELSE,                      @"else" },                                      //  else
            {TokenType.T_ELSEIF,                    @"elseif" },                                    //  elseif
            {TokenType.T_ENDIF,                     @"endif" },                                     //  endif

            //  try .. catch
            {TokenType.T_THROW,                     @"throw" },                                     //  throw
            {TokenType.T_TRY,                       @"try" },                                       //  try
            {TokenType.T_CATCH,                     @"catch" },                                     //  catch
            {TokenType.T_FINALLY,                   @"finally" },                                   //  finally

            //  Magic Constants
            {TokenType.T_FILE,                      @"__[Ff][Ii][Ll][Ee]__" },                      //  __FILE__
            {TokenType.T_FUNC_C,                    @"__[Ff][Uu][Nn][Cc][Tt][Ii][Oo][Nn]__" },      //  __FUNCTION__
            {TokenType.T_CLASS_C,                   @"__[Cc][Ll][Aa][Ss][Ss]__" },                  //  __CLASS__
            {TokenType.T_DIR,                       @"__[Dd][Ii][Rr]__" },                          //  __DIR__
            {TokenType.T_TRAIT_C,                   @"__[Tt][Rr][Aa][Ii][Tt]__" },                  //  __TRAIT__
            {TokenType.T_LINE,                      @"__[Ll][Ii][Nn][Ee]__" },                      //  __LINE__
            {TokenType.T_METHOD_C,                  @"__[Mm][Ee][Tt][Hh][Oo][Dd]__" },              //  __METHOD__
            {TokenType.T_NS_C,                      @"__[Nn][Aa][Mm][Ee][Ss][Pp][Aa][Cc][Ee]__" },  //  __NAMESPACE__

            //  Braces
            {TokenType.T_BRACE_OPEN,                @"\(" },                                        //  (
            {TokenType.T_BRACE_CLOSE,               @"\)" },                                        //  )
            {TokenType.T_CURLY_BRACE_OPEN,          @"\{" },                                        //  {
            {TokenType.T_CURLY_BRACE_CLOSE,         @"\}" },                                        //  }
            {TokenType.T_SQUARE_BRACE_OPEN,         @"\[" },                                        //  ]
            {TokenType.T_SQUARE_BRACE_CLOSE,        @"\]" },                                        //  ]

            //  Language Constructions
            {TokenType.T_CONST,                     @"const" },                                     //  const
            {TokenType.T_ECHO,                      @"echo" },                                      //  echo
            {TokenType.T_MATCH,                     @"match" },                                     //  match
            {TokenType.T_GLOBAL,                    @"global" },                                    //  global
            {TokenType.T_DECLARE,                   @"declare" },                                   //  declare
            {TokenType.T_ENDDECLARE,                @"enddeclare" },                                //  enddeclare

            //  Namespaces
            {TokenType.T_NAMESPACE,                 @"namespace" },                                 //  namespace
            {TokenType.T_NAMESPACE_NAME,            @"([a-zA-Z_][a-zA-Z0-9_]*)((\\[a-zA-Z_][a-zA-Z0-9_]*)+)" },                                        //  a\b\c
            {TokenType.T_NAMESPACE_CALL_NAME,       @"(\\[a-zA-Z_][a-zA-Z0-9_]*)+" },                                          //  \a\b\c
            {TokenType.T_USE,                       @"use" },                                       //  use

            //  Goto points
            {TokenType.T_GOTO,                      @"goto" },                                      //  goto
            {TokenType.T_COLON,                     @":" },                                         //  :

            //  Including Operators
            {TokenType.T_INCLUDE,                   @"include" },                                   //  include
            {TokenType.T_INCLUDE_ONCE,              @"include_once" },                              //  include_once
            {TokenType.T_REQUIRE,                   @"require" },                                   //  require
            {TokenType.T_REQUIRE_ONCE,              @"require_once" },                              //  require_once


            //  Unused Tokens
            {TokenType.T_DOC_COMMENT,               @"\/\*\*[\s\S]*\*\/"},                          // /** */
            {TokenType.T_COMMENT,                   @"(((\/\/)|#).*)|(\/\*[\s\S]*\*\/)"},           // // # /* */
            {TokenType.T_WHITESPACE,                @"[\s]+"},                      //  \t \r \n

            //  Basic Functions
            //  {TokenType.T_UNSET, "TODO"},                    //  unset()
            //  {TokenType.T_EMPTY, "TODO"},                    //  empty()
            //  {TokenType.T_EVAL, "TODO"},                     //  eval()
            //  {TokenType.T_EXIT, "TODO"},                     //  exit() die()
            //  {TokenType.T_HALT_COMPILER, "TODO"},            //  __halt_compiler()
            //  {TokenType.T_ISSET, "TODO"},                    //  isset()
            //  {TokenType.T_LIST, "TODO"},                     //  list()
            //  {TokenType.T_PRINT, "TODO"},                    //  print()
            
            //  PHP Code Limiters
            {TokenType.T_OPEN_TAG,                  @"[<](([?]([pP][hH][pP])?)|[%])" },             //  <? <?php <%
            {TokenType.T_OPEN_TAG_WITH_ECHO,        @"<[?%]=" },                                    //  <?= <%=
            {TokenType.T_CLOSE_TAG,                 @"[?%][>]" },                                   //  ?> %>
            //  {TokenType.T_INLINE_HTML,               @"" },              //  any text outside php code tags

            //  Separators
            {TokenType.T_COMMA,                     @"[,]" },                                       //  ,
            {TokenType.T_SEMICOLON,                 @";" },                                         //  ;

            //  Heredoc
            {TokenType.T_START_HEREDOC,                 @"<<<" },                                   //  <<<
            {TokenType.T_END_HEREDOC,                   @"END" }                                    //  END

            //  Incorrect symbols
            //  {TokenType.T_BAD_CHARACTER,             @""},            //  0x00-0x1F without \t (0x09), "TODO"}, \n (0x0a) и \r (0x0d)
			};

        public static Regex GetPatternRegex(this TokenType type)
        {
            return new Regex(GetPatternString(type));
        }

        public static string GetPatternString(this TokenType type)
        {
            return "^(" + patterns[type] + ")";
        }
    }
}
