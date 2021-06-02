﻿using System;
using System.Collections.Generic;

namespace Reinforced.Typings.Ast
{
    /// <summary>
    /// AST node for JSDOC documentation
    /// </summary>
    public class RtJsdocNode : RtNode
    {
        /// <summary>
        /// Main documentation text
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Additional JSDOC documentation tags
        /// </summary>
        public List<Tuple<DocTag, string>> TagToDescription { get; private set; }

        /// <summary>
        /// Constructs new instance of AST node
        /// </summary>
        public RtJsdocNode()
        {
            TagToDescription = new List<Tuple<DocTag, string>>();
        }

        /// <inheritdoc />
        public override IEnumerable<RtNode> Children
        {
            get { yield break; }
        }

        /// <inheritdoc />
        public override void Accept(IRtVisitor visitor)
        {
            visitor.Visit(this);
        }

        /// <inheritdoc />
        public override void Accept<T>(IRtVisitor<T> visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Adds an additional JSDOC documentation tag.
        /// </summary>
        public void AddTag(DocTag tag, string value = null) =>
            TagToDescription.Add(new Tuple<DocTag, string>(tag, value));
    }
}
