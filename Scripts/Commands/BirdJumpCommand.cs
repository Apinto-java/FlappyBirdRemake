using Godot;

public partial class BirdJumpCommand : BirdCommand
{
    public override void Execute(Player player, GodotObject data)
    {
        if(data is not BirdJumpParams)
            return;

        player.Jump();
    }
}

public partial class BirdJumpParams : GodotObject
{

}