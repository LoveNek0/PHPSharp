using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Token
{
    public static class TokenQueue
    {
        private static readonly TokenType[] queue = new TokenType[]
        {
        TokenType.T_VARIABLE,                 //  $var_1
        TokenType.T_DOLLAR_OPEN_CURLY_BRACES, //  ${
        TokenType.T_COMMA,                    //  ,
        TokenType.T_SEMICOLON,                //  ;
        TokenType.T_DOC_COMMENT,              // /** */
        TokenType.T_COMMENT,                  // // # /* */
        TokenType.T_START_HEREDOC,            //  <<<
        TokenType.T_OPEN_TAG,                 //  <? <?php <%
        TokenType.T_OPEN_TAG_WITH_ECHO,       //  <?= <%=
        TokenType.T_CLOSE_TAG,                //  ?> %>
        TokenType.T_BIT_SHIFT_LEFT,           //  <<
        TokenType.T_BIT_SHIFT_RIGHT,          //  >>
        TokenType.T_BIT_AND_EQUAL,            //  &=
        TokenType.T_BIT_XOR_EQUAL,            //  ^=
        TokenType.T_BIT_NOT_EQUAL,            //  |=
        TokenType.T_BIT_SHIFT_LEFT_EQUAL,     //  <<=
        TokenType.T_BIT_SHIFT_RIGHT_EQUAL,    //  >>=
        TokenType.T_PLUS_EQUAL,               //  +=
        TokenType.T_MINUS_EQUAL,              //  -=
        TokenType.T_MUL_EQUAL,                //  *=
        TokenType.T_DIV_EQUAL,                //  /=
        TokenType.T_MOD_EQUAL,                //  %=
        TokenType.T_POW_EQUAL,                //  **=
        TokenType.T_COALESCE,                 //  ??
        TokenType.T_POW,                      //  **
        TokenType.T_CONCAT_EQUAL,             //  .=
        TokenType.T_COALESCE_EQUAL,           //  ??=
        TokenType.T_DEC,                      //  --
        TokenType.T_INC,                      //  ++
        TokenType.T_LOGICAL_AND,              //  && and
        TokenType.T_LOGICAL_OR,               //  || or
        TokenType.T_IS_EQUAL,                 //  ==
        TokenType.T_DOUBLE_ARROW,             //  =>
        TokenType.T_NAMESPACE_NAME,           //  a\b\c
        TokenType.T_NAMESPACE_CALL_NAME,      //  \a\b\c
        TokenType.T_IS_IDENTICAL,             //  ===
        TokenType.T_IS_NOT_EQUAL,             //  != <>
        TokenType.T_IS_NOT_IDENTICAL,         //  !==
        TokenType.T_IS_GREATER_OR_EQUAL,      //  >=
        TokenType.T_IS_SMALLER_OR_EQUAL,      //  <=
        TokenType.T_SPACESHIP,                //  <=>
        TokenType.T_DOUBLE_COLON,             //  ::
        TokenType.T_OBJECT_OPERATOR,          //  ->
        TokenType.T_NULLSAFE_OBJECT_OPERATOR, //  ?->
        TokenType.T_ELLIPSIS,                 //  ...
        TokenType.T_ADD,                      //  +
        TokenType.T_SUB,                      //  -
        TokenType.T_MUL,                      //  *
        TokenType.T_DIV,                      //  /
        TokenType.T_MOD,                      //  %
        TokenType.T_BIT_AND,                  //  &
        TokenType.T_BIT_XOR,                  //  ^
        TokenType.T_BIT_NOT,                  //  |
        TokenType.T_CONCAT,                   //  .
        TokenType.T_ASSIGNMENT,                    //  =
        TokenType.T_IS_GREATER,               // >
        TokenType.T_IS_SMALLER,               // <
        TokenType.T_LNUMBER,                  //  123 012 0x1ac
        TokenType.T_DNUMBER,                  //  1.5
        TokenType.T_LOGICAL_XOR,              //  xor
        TokenType.T_FUNCTION,                 //  function
        TokenType.T_STATIC,                   //  static
        TokenType.T_RETURN,                   //  return
        TokenType.T_YIELD,                    //  yield
        TokenType.T_YIELD_FROM,               //  yield_from
        TokenType.T_CALLABLE,                 //  callable
        TokenType.T_FN,                       //  fn
        TokenType.T_ABSTRACT,                 //  abstract
        TokenType.T_INTERFACE,                //  interface
        TokenType.T_TRAIT,                    //  trait
        TokenType.T_CLASS,                    //  class
        TokenType.T_PUBLIC,                   //  public
        TokenType.T_PRIVATE,                  //  private
        TokenType.T_PROTECTED,                //  protected
        TokenType.T_CLONE,                    //  clone
        TokenType.T_EXTENDS,                  //  extends
        TokenType.T_IMPLEMENTS,               //  implements
        TokenType.T_FINAL,                    //  final
        TokenType.T_INSTANCEOF,               //  instanceof
        TokenType.T_INSTEADOF,                //  insteadof
        TokenType.T_NEW,                      //  new
        TokenType.T_CONST,                    //  const
        TokenType.T_ECHO,                     //  echo
        TokenType.T_MATCH,                    //  match
        TokenType.T_GLOBAL,                   //  global
        TokenType.T_DECLARE,                  //  declare
        TokenType.T_ENDDECLARE,               //  enddeclare
        TokenType.T_NAMESPACE,                //  namespace
        TokenType.T_USE,                      //  use
        TokenType.T_GOTO,                     //  goto
        TokenType.T_INCLUDE,                  //  include
        TokenType.T_INCLUDE_ONCE,             //  include_once
        TokenType.T_REQUIRE,                  //  require
        TokenType.T_REQUIRE_ONCE,             //  require_once
        TokenType.T_DO,                       //  do
        TokenType.T_WHILE,                    //  while
        TokenType.T_ENDWHILE,                 //  endwhile
        TokenType.T_FOR,                      //  for
        TokenType.T_ENDFOR,                   //  endfor
        TokenType.T_FOREACH,                  //  foreach
        TokenType.T_ENDFOREACH,               //  endforeach
        TokenType.T_AS,                       //  as
        TokenType.T_CONTINUE,                 //  continue
        TokenType.T_SWITCH,                   //  switch
        TokenType.T_ENDSWITCH,                //  endswitch
        TokenType.T_CASE,                     //  case
        TokenType.T_BREAK,                    //  break
        TokenType.T_DEFAULT,                  //  default
        TokenType.T_IF,                       //  if
        TokenType.T_ELSE,                     //  else
        TokenType.T_ELSEIF,                   //  elseif
        TokenType.T_ENDIF,                    //  endif
        TokenType.T_THROW,                    //  throw
        TokenType.T_END_HEREDOC,               //  END
        TokenType.T_TRY,                      //  try
        TokenType.T_CATCH,                    //  catch
        TokenType.T_FINALLY,                  //  finally
        TokenType.T_INT_CAST,                 //  (int) (integer)
        TokenType.T_DOUBLE_CAST,              //  (real) (double) (float)
        TokenType.T_STRING_CAST,              //  (string)
        TokenType.T_BOOL_CAST,                //  (bool) (boolean)
        TokenType.T_ARRAY_CAST,               //  (array)
        TokenType.T_OBJECT_CAST,              //  (object)
        TokenType.T_UNSET_CAST,               //  (unset)
        TokenType.T_BRACE_OPEN,               //  (
        TokenType.T_BRACE_CLOSE,              //  )
        TokenType.T_CURLY_BRACE_OPEN,         //  {  
        TokenType.T_CURLY_BRACE_CLOSE,        //  }
        TokenType.T_SQUARE_BRACE_OPEN,        //  [
        TokenType.T_SQUARE_BRACE_CLOSE,       //  ]
        TokenType.T_ARRAY,                    //  array()
        TokenType.T_FILE,                     //  __FILE__
        TokenType.T_FUNC_C,                   //  __FUNCTION__
        TokenType.T_CLASS_C,                  //  __CLASS__
        TokenType.T_DIR,                      //  __DIR__
        TokenType.T_TRAIT_C,                  //  __TRAIT__
        TokenType.T_LINE,                     //  __LINE__
        TokenType.T_METHOD_C,                 //  __METHOD__
        TokenType.T_NS_C,                     //  __NAMESPACE__
        TokenType.T_CONSTANT_ENCAPSED_STRING, //  "" ''
        TokenType.T_STRING,                   //  parent self T_CONSTANT_ENCAPSED_STRING
        TokenType.T_COLON,                    //  :
        TokenType.T_WHITESPACE                //  \t \r \n

        //  T_ATTRIBUTE,                                //  attribytes

        //  Basic Functions
        //  T_UNSET,                    //  unset()
        //  T_EMPTY,                    //  empty()
        //  T_EVAL,                     //  eval()
        //  T_EXIT,                     //  exit() die()
        //  T_HALT_COMPILER,            //  __halt_compiler()
        //  T_ISSET,                    //  isset()
        //  T_LIST,                     //  list()
        //  T_PRINT,                    //  print()

        

        //  Incorrect symbols
        //  T_BAD_CHARACTER, \n (0x0a) и \r (0x0d)
		};

        public static TokenType[] GetQueue()
        {
            return queue;
        }
    }
}
