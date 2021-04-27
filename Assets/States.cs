using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayerState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Think()
    {
        base.Think();
    }

    public override void Exit()
    {
        base.Exit();
    }

}

public class LookAtPlayerState : State
{
    GameObject player = Camera.main.gameObject;
        
    public override void Enter()
    {
        owner.GetComponent<LookAt>().enabled = true;
    }

    public override void Think()
    {
        owner.GetComponent<LookAt>().lookTarget = player;
    }

    public override void Exit()
    {
        owner.GetComponent<LookAt>().lookTarget = null;
        owner.GetComponent<LookAt>().enabled = false;
    }
}

public class FetchBallState : State
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void Think()
    {
        base.Think();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class PickupBallState : State
{
        public override void Enter()
    {
        base.Enter();
    }

    public override void Think()
    {
        base.Think();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

public class DropBallState : State
{
        public override void Enter()
    {
        base.Enter();
    }

    public override void Think()
    {
        base.Think();
    }

    public override void Exit()
    {
        base.Exit();
    }
}


public class DogActive : State
{
    public override void Think()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();
        GameObject ballParent = null;
        if (null != ball)
            ballParent = ball.transform.parent.gameObject;

        
        // if ball doesn't exist and dog near player
        if (null == ball && Vector3.Distance(owner.transform.position, player.transform.position) <= 10.0f)
        {
            // look at player
            owner.ChangeState(new LookAtPlayerState());
        }
        
        // if ball doesn't exists and dog far from player
        if (null == ball && Vector3.Distance(owner.transform.position, player.transform.position) > 10.0f)
        {
            // come to player
            owner.ChangeState(new SeekPlayerState());
        }

        // if ball exists and in the wild      
        if (null != ball &&  null == ballParent)
        {
            // if dog close to the ball (within 1.5 scales of it's body)
            GameObject dogBody = owner.transform.Find("dog").gameObject;
            if (Vector3.Distance(ball.transform.position, owner.transform.position) < dogBody.transform.localScale.z * 1.5f)
                // pickup the ball
                owner.ChangeState(new PickupBallState());
            else
                // go and fetch the ball
                owner.ChangeStateDelayed(new FetchBallState(),1);
        }
        
        // if ball exists and in dog's mouth
        if (null != ball && null != ballParent)
        {
            // if alreday close to the player
            if (Vector3.Distance(owner.transform.position, player.transform.position) <= 10.0f)
                // drop the ball
                owner.ChangeState(new DropBallState());
            else
                // return to player
                owner.ChangeState(new SeekPlayerState());
        }
    }
}
