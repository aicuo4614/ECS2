using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviorTree
{

    public enum NodeType
    {
        //复合节点类型
        Sequence,   //顺序节点 &
        Selector,   //选择节点 |

        //叶子节点类型
        Wait,   //等待节点
        Action, //动作节点
        Condition,  //条件节点
    }

    /// <summary>
    /// 节点状态
    /// </summary>
    public enum NodeState
    {
        Ready,
        Running,
        Faied,
        Success,
    }



    public class BehaviorTree
    {
        protected readonly BtNode rootNode;

        protected NodeState nodeState;
        public BehaviorTree(BtNode root)
        {
            rootNode = root;
        }

        public NodeState Update()
        {
            nodeState = rootNode.OnVisit();
            return nodeState;
        }

        public void Reset()
        {
            rootNode.Reset();
        }



    }

    /// <summary>
    /// 节点基类
    /// </summary>
    public abstract class BtNode
    {
        public NodeType Type { get; protected set; }
        public NodeState State { get; protected set; }

        protected BtNode(NodeType nodeType)
        {
            State = NodeState.Ready;
            Type = nodeType;
        }

        public abstract NodeState OnVisit();

        public virtual void Reset()
        {
            State = NodeState.Ready;
        }

    }

    /// <summary>
    /// 复合节点基类
    /// </summary>
    public abstract class BTCompositeNode : BtNode
    {
        /// <summary>
        /// 子节点
        /// </summary>
        protected List<BtNode> ChildNodes;

        protected BTCompositeNode(List<BtNode> nodes, NodeType nodeType) : base(nodeType)
        {

            ChildNodes = nodes ?? new List<BtNode> { };
        }

        public virtual BTCompositeNode AddChild(BtNode node)
        {
            ChildNodes.Add(node);
            return this;
        }

    }

    #region 叶子节点

    /// <summary>
    /// 动作节点
    /// </summary>
    public class ActionNode : BtNode
    {
        private readonly Action action;
        public ActionNode(Action action) : base(NodeType.Action)
        {
            this.action = action;
        }

        public override NodeState OnVisit()
        {
            action?.Invoke();
            return NodeState.Success;
        }
    }

    /// <summary>
    /// 条件节点
    /// </summary>
    public class ConditionNode : BtNode
    {
        private readonly Func<bool> checkFunc;
        public ConditionNode(Func<bool> checkFunc) : base(NodeType.Condition)
        {
            this.checkFunc = checkFunc;

        }

        public override NodeState OnVisit()
        {
            if (this.checkFunc == null)
            {
                return NodeState.Success;
            }
            var result = this.checkFunc.Invoke();
            return result ? NodeState.Success : NodeState.Faied;
        }

    }

    /// <summary>
    /// 等待节点
    /// </summary>
    public class WaitNode : BtNode
    {
        long _startSec;
        int waitForSecond;

        public WaitNode(int waitForSecond) : base(NodeType.Wait)
        {
            this.waitForSecond = waitForSecond;
        }

        public override NodeState OnVisit()
        {
            if (_startSec == 0)
            {
                _startSec = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                State = NodeState.Running;
            }
            if (DateTime.Now < new DateTime((waitForSecond + _startSec) * TimeSpan.TicksPerSecond))
                return State;
            State = NodeState.Success;
            return State;
        }



        public override void Reset()
        {
            base.Reset();
            _startSec = 0;
        }

    }

    #endregion

    #region 复合节点


    public class SequenceNode : BTCompositeNode
    {
        public SequenceNode(List<BtNode> nodes = null) : base(nodes, NodeType.Sequence)
        {
        }

        public override NodeState OnVisit()
        {
            foreach (var node in ChildNodes)
            {
                var result = node.OnVisit();
                if (result != NodeState.Success)
                    return result;
            }

            return NodeState.Success;
        }
    }

    public class SelectNode : BTCompositeNode
    {
        public SelectNode(List<BtNode> nodes = null) : base(nodes, NodeType.Selector)
        {
        }

        public override NodeState OnVisit()
        {
            foreach (var node in ChildNodes)
            {
                var result = node.OnVisit();
                if (result != NodeState.Faied)
                    return result;
            }

            return NodeState.Faied;
        }
    }

    #endregion



}
