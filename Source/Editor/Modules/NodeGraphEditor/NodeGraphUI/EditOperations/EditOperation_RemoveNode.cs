﻿using EditorUI;
using System.Collections.Generic;

namespace CrossEditor
{
    class EditOperation_RemoveNode : EditOperation
    {
        NodeGraphView _View;
        List<Node> _Nodes;
        List<Connection> _ConnectionsToRemove;

        public EditOperation_RemoveNode(NodeGraphView View, List<Node> Nodes, List<Connection> ConnectionsToRemove)
        {
            _View = View;
            _Nodes = Nodes.Clone();
            _ConnectionsToRemove = ConnectionsToRemove.Clone();
        }

        public override void Undo()
        {
            _View.ClearSelectedNodes();
            foreach (Node Node in _Nodes)
            {
                _View.GetModel().AddNode(Node);
                _View.AddSelectedNode(Node);
            }

            foreach (Connection Connection in _ConnectionsToRemove)
            {
                _View.GetModel().AddConnection(Connection);
            }

            _View.SetModified();
        }

        public override void Redo()
        {
            foreach (Connection Connection in _ConnectionsToRemove)
            {
                _View.GetModel().RemoveConnection(Connection);
            }

            _View.ClearSelectedNodes();
            foreach (Node Node in _Nodes)
            {
                _View.GetModel().RemoveNode(Node);
            }

            _View.SetModified();
        }
    }
}
