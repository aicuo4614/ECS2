using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTreeTest;
public class BehaviorTreeSample : MonoBehaviour
{
    BehaviorTreeTest.BehaviorTree behaviorTree;
    // 初始化调用
    void Start()
    {


        //看电影
        var watchMovie = new SequenceNode().AddChild(new ConditionNode(() => { return EnoughPayMovie(); }))
                                           .AddChild(new ActionNode(() => { Debug.Log("买了电影票后，我们在看电影"); }));

        //吃饭
        var eat = new SequenceNode().AddChild(new ConditionNode(() => { Debug.Log("我们只有吃饭的钱。"); return EnoughEat(); }))
                                    .AddChild(new ActionNode(() => { Debug.Log("我们正在等待上菜"); }))
                                    .AddChild(new WaitNode(3))
                                    .AddChild(new ActionNode(() => { Debug.Log("我和女朋友吃饭呢！！"); }));


        //打dota
        var playGame = new ActionNode(() => { Debug.Log("没有女朋友也没有钱，我就在家里打DOTA!"); });



        var behaviorHasGirlFriend = new SelectNode().AddChild(watchMovie).AddChild(eat);

        var behaviorHandleHasGirlFriend = new SequenceNode().AddChild(new ConditionNode(() => { return HasGirlFriend(); })).AddChild(behaviorHasGirlFriend);

        var root = new SelectNode().AddChild(behaviorHandleHasGirlFriend).AddChild(playGame);

        behaviorTree = new BehaviorTreeTest.BehaviorTree(root);


        //var root = new SelectNode().AddChild(new SequenceNode().AddChild(new ConditionNode(() => { return HasGirlFriend(); }))
        //                                                       .AddChild(new ActionNode(() => { Debug.Log("我和我的女朋友我们一起出门"); }))
        //                                                       .AddChild(new SelectNode().AddChild(new ConditionNode(() => { return EnoughPayMovie(); }))))
        //                           .AddChild(new ActionNode(() => { Debug.Log("没有女朋友的我一个人在家里打Doa"); }));


        behaviorTree.Update();
    }

    private void Update()
    {
        if (Execute)
        {
            behaviorTree.Update();
            Execute = false;
        }
    }

    public bool IsEnoughEat;
    public bool IsHasGirlFriend;
    public bool IsEnoughPayMovie;

    public bool Execute;

    bool EnoughEat()
    {
        if (IsEnoughEat)
        {
            Debug.Log("吃饭钱够");
        }
        else
        {
            Debug.Log("吃饭钱不够");
        }
        return IsEnoughEat;
    }

    bool HasGirlFriend()
    {
        if (IsHasGirlFriend)
        {
            Debug.Log("我想起来我有个女朋友");
        }
        else
        {
            Debug.Log("我没有女朋友");
        }
        return IsHasGirlFriend;
    }

    bool EnoughPayMovie()
    {
        if (IsEnoughPayMovie)
        {
            Debug.Log("看电影钱够");
        }
        else
        {
            Debug.Log("看电影钱不够");
        }
        return IsEnoughPayMovie;
    }


}
