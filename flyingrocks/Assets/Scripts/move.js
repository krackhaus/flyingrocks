
function Update () 
{
	if(Input.GetKey("up"))
	{
		this.transform.position.z+=0.1;	
	}
	else if(Input.GetKey("down"))
	{
		this.transform.position.z-=0.1;	
	}
	else if(Input.GetKey("left"))
	{
		this.transform.position.x-=0.1;	
	}
	else if(Input.GetKey("right"))
	{
		this.transform.position.x+=0.1;	
	}

}