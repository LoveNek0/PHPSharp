using PHP.Core.Lang;
using PHP.Core.Lang.AST.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHP.Core.Lang.AST.Parsers
{
    public class WhileParser
    {
        private ASTBuilder builder;

        public WhileParser(ASTBuilder builder) => this.builder = builder;



        public ASTWhileNode Parse()
        {
            return null;
        }
    }
}
