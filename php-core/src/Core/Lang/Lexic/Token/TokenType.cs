using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.Lexic.Token
{
    public enum TokenType
    {
        //  Arithmetic operators
        T_ADD,                      //  +
        T_SUB,                      //  -
        T_MUL,                      //  *
        T_DIV,                      //  /
        T_MOD,                      //  %
        T_POW,                      // **

        //  Assignment operators
        T_EQUAL,                    //  =

        T_ABSTRACT,                 //  abstract
        T_ARRAY,                    //  array
        T_ARRAY_BRACE_OPEN,         //  [
        T_ARRAY_BRACE_CLOSE,        //  ]

        T_AND_EQUAL,                //  &=
        T_ARRAY_CAST,               //  (array)
        T_AS,                       //  as
        T_ATTRIBUTE,                //  attribytes
        T_BAD_CHARACTER,            //  0x00-0x1F without \t (0x09), \n (0x0a) и \r (0x0d)
        T_BOOLEAN_AND,              //  &&
        T_BOOLEAN_OR,               //  ||
        T_BOOL_CAST,                //  (bool) (boolean)
        T_BREAK,                    //  break
        T_CALLABLE,                 //  callable
        T_CASE,                     //  case
        T_CATCH,                    //  catch
        T_CLASS,                    //  class
        T_CLASS_C,                  //  __CLASS__
        T_CLONE,                    //  clone
        T_CLOSE_TAG,                //  ?> %>
        T_COALESCE_EQUAL,           //  ??=
        T_COALESCE,                 //  ??
        T_COMMENT,                  //  // # /**/
        T_CONCAT_EQUAL,             //  .=
        T_CONST,                    //  const
        T_CONSTANT_ENCAPSED_STRING, //  "" ''
        T_CONTINUE,                 //  continue
        T_CURLY_OPEN,               //  {$
        T_DEC,                      //  --
        T_DECLARE,                  //  declare
        T_DEFAULT,                  //  default
        T_DIR,                      //  __DIR__
        T_DIV_EQUAL,                //  /=
        T_DNUMBER,                  //  0.123
        T_DO,                       //  do
        T_DOC_COMMENT,              //  /** */
        T_DOLLAR_OPEN_CURLY_BRACES, //  ${
        T_DOUBLE_ARROW,             //  =>
        T_DOUBLE_CAST,              //  (real) (double) (float)
        T_DOUBLE_COLON,             //  ::
        T_ECHO,                     //  echo
        T_ELLIPSIS,                 //  ...
        T_ELSE,                     //  else
        T_ELSEIF,                   //  elseif
        T_EMPTY,                    //  empty()
        T_ENCAPSED_AND_WHITESPACE,  //  "$var"
        T_ENDDECLARE,               //  enddeclare
        T_ENDFOR,                   //  endfor
        T_ENDFOREACH,               //  endforeach
        T_ENDIF,                    //  endif
        T_ENDSWITCH,                //  endswitch
        T_ENDWHILE,                 //  endwhile
        T_END_HEREDOC,              //  END
        T_EVAL,                     //  eval()
        T_EXIT,                     //  exit() die()
        T_EXTENDS,                  //  extends
        T_FILE,                     //  __FILE__
        T_FINAL,                    //  final
        T_FINALLY,                  //  finally
        T_FN,                       //  fn
        T_FOR,                      //  for
        T_FOREACH,                  //  foreach
        T_FUNCTION,                 //  function
        T_FUNC_C,                   //  __FUNCTION__
        T_GLOBAL,                   //  global
        T_GOTO,                     //  goto
        T_HALT_COMPILER,            //  __halt_compiler()
        T_IF,                       //  if
        T_IMPLEMENTS,               //  implements
        T_INC,                      //  ++
        T_INCLUDE,                  //  include
        T_INCLUDE_ONCE,             //  include_once
        T_INLINE_HTML,              //  any text outside php code tags
        T_INSTANCEOF,               //  instanceof
        T_INSTEADOF,                //  insteadof
        T_INTERFACE,                //  interface
        T_INT_CAST,                 //  (int) (integer)
        T_ISSET,                    //  isset()
        T_IS_EQUAL,                 //  ==
        T_IS_GREATER_OR_EQUAL,      //  >=
        T_IS_IDENTICAL,             //  ===
        T_IS_NOT_EQUAL,             //  != <>
        T_IS_NOT_IDENTICAL,         //  !==
        T_IS_SMALLER_OR_EQUAL,      //  <=
        T_LINE,                     //  __LINE__
        T_LIST,                     //  list()
        T_LNUMBER,                  //  123 012 0x1ac
        T_LOGICAL_AND,              //  and
        T_LOGICAL_OR,               //  or
        T_LOGICAL_XOR,              //  xor
        T_MATCH,                    //  match
        T_METHOD_C,                 //  __METHOD__
        T_MINUS_EQUAL,              //  -=
        T_MOD_EQUAL,                //  %=
        T_MUL_EQUAL,                //  *=
        T_NAMESPACE,                //  namespace
        T_NAME_FULLY_QUALIFIED,     //  \App\Namespace
        T_NEW,                      //  new
        T_NS_C,                     //  __NAMESPACE__
        T_NS_SEPARATOR,             //  \
        T_NUM_STRING,               //  "$var[0]"
        T_OBJECT_CAST,              //  (object)
        T_OBJECT_OPERATOR,          //  ->
        T_NULLSAFE_OBJECT_OPERATOR, //  ?->
        T_OPEN_TAG,                 //  <?php <? <%
        T_OPEN_TAG_WITH_ECHO,       //  <?= <%=
        T_OR_EQUAL,                 //  |=
        T_PAAMAYIM_NEKUDOTAYIM,     //  ::
        T_PLUS_EQUAL,               //  +=
        T_POW_EQUAL,                //  **=
        T_PRINT,                    //  print()
        T_PRIVATE,                  //  private
        T_PROTECTED,                //  protected
        T_PUBLIC,                   //  public
        T_REQUIRE,                  //  require
        T_REQUIRE_ONCE,             //  require_once
        T_RETURN,                   //  return
        T_SL,                       //  <<
        T_SL_EQUAL,                 //  <<=
        T_SPACESHIP,                //  <=>
        T_SR,                       //  >>
        T_SR_EQUAL,                 //  >>=
        T_START_HEREDOC,            //  <<<
        T_STATIC,                   //  static
        T_STRING,                   //  parent self T_CONSTANT_ENCAPSED_STRING
        T_STRING_CAST,              //  (string)
        T_STRING_VARNAME,           //  "${a
        T_SWITCH,                   //  switch
        T_THROW,                    //  throw
        T_TRAIT,                    //  trait
        T_TRAIT_C,                  //  __TRAIT__
        T_TRY,                      //  try
        T_UNSET,                    //  unset()
        T_UNSET_CAST,               //  (unset)
        T_USE,                      //  use
        T_VAR,                      //  var
        T_VARIABLE,                 //  $var
        T_WHILE,                    //  while
        T_WHITESPACE,               //  \t \r \n
        T_XOR_EQUAL,                //  ^=
        T_YIELD,                    //  yield
        T_YIELD_FROM,               //  yield_from

        T_SEMICOLON,                //  ;
        T_CURLY_BRACE_OPEN,         //  {
        T_CURLY_BRACE_CLOSE,        //  }
        T_BRACE_OPEN,               //  (
        T_BRACE_CLOSE,              //  )
        

        T_COMMA,                    //  ,
        T_DOT                       //  .
    }
}
