using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayerState : State
{
    public override void Enter()
    {
        GameObject player = Camera.main.gameObject;
        owner.GetComponent<Seek>().targetGameObject = player;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();
        GameObject ballHolder = null;

        if (null != ball && null != ball.transform.parent)
            ballHolder = ball.transform.parent.gameObject;
        
        // if ball exists and not in dog's mouth
        if (null != ball && null == ballHolder)
            // chnage state to Fetch
            owner.ChangeState(new FetchBallState());

        // if dog close to the player and ball is in the mouth
        if (Vector3.Distance(owner.transform.position, player.transform.position) <= 10.0f && null != ballHolder)
            // change state to Drop Ball
            owner.ChangeState(new DropBallState());

        // if dog close to the player and no ball in the mouth
        if (Vector3.Distance(owner.transform.position, player.transform.position) <= 10.0f && null == ballHolder)
            // change state to Look at Player
            owner.ChangeState(new LookAtPlayerState());

    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().targetGameObject = null;
        owner.GetComponent<Seek>().enabled = false;
        // add proverbal handbrake
        owner.GetComponent<Boid>().acceleration = new Vector3(0.0f,0.0f,0.0f);
        owner.GetComponent<Boid>().velocity = new Vector3(0.0f,0.0f,0.0f);
        owner.GetComponent<Boid>().force = new Vector3(0.0f,0.0f,0.0f);
    }

}

public class LookAtPlayerState : State
{        
    public override void Enter()
    {
        GameObject player = Camera.main.gameObject;
        owner.GetComponent<LookAt>().lookTarget = player;
        owner.GetComponent<LookAt>().enabled = true;
    }

    public override void Think()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();

        owner.GetComponent<LookAt>().lookTarget = player;

        // if ball exists and not in dog's mouth
        if (null != ball)
            // chnage state to Fetch
            owner.ChangeState(new FetchBallState());

        // if dog far away from the player
        if (Vector3.Distance(owner.transform.position, player.transform.position) > 10.0f)
            // change state to Seek Player
            owner.ChangeState(new SeekPlayerState());
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
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();

        // play bark sound
        owner.GetComponent<Barking>().Bark();

        // start fetching
        owner.GetComponent<Seek>().targetGameObject = ball;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();
        GameObject dogBody = owner.transform.Find("dog").gameObject;

        owner.GetComponent<Seek>().targetGameObject = ball;

        // if ball in vicinity (max 1.1 of dog size) and ball is lying (less than it's size)
        if (Vector3.Distance(ball.transform.position, owner.transform.position) < dogBody.transform.localScale.z * 1.1f &&
                ball.transform.position.y < ball.transform.localScale.y)
            // pickup ball
            owner.ChangeState(new PickupBallState());
    }

    public override void Exit()
    {
        owner.GetComponent<Seek>().targetGameObject = null;
        owner.GetComponent<Seek>().enabled = false;
        // add proverbal handbrake
        owner.GetComponent<Boid>().acceleration = new Vector3(0.0f,0.0f,0.0f);
        owner.GetComponent<Boid>().velocity = new Vector3(0.0f,0.0f,0.0f);
        owner.GetComponent<Boid>().force = new Vector3(0.0f,0.0f,0.0f);
    }
}

public class PickupBallState : State
{
    public override void Enter()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();
        owner.GetComponent<PickUpAndDrop>().enabled = true;
        owner.GetComponent<PickUpAndDrop>().PickUp(ball);
    }

    public override void Think()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();

        // if ball successfully picked up
        if (owner.GetComponent<PickUpAndDrop>().ballPicked)
            // return to player
            owner.ChangeState(new SeekPlayerState());

    }

    public override void Exit()
    {
        owner.GetComponent<PickUpAndDrop>().enabled = false;
    }
}

public class DropBallState : State
{
    public override void Enter()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();
        owner.GetComponent<PickUpAndDrop>().enabled = true;
        owner.GetComponent<PickUpAndDrop>().Drop();
    }

    public override void Think()
    {
        GameObject player = Camera.main.gameObject;
        GameObject ball = player.GetComponent<FPSController>().GetBall();

        // move back to seek player if ball was dropped
        if (!owner.GetComponent<PickUpAndDrop>().ballPicked)
             owner.ChangeState(new SeekPlayerState());
    }

    public override void Exit()
    {
        owner.GetComponent<PickUpAndDrop>().enabled = false;
    }
}
