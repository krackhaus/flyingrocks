var crow: GameObject;
var numberOfBirds = 25;
var rangeFromOrigin = 5;
var startingElevationAboveGround = 5;

function Start()
{
	//create crows
	for(var i = 0; i < numberOfBirds; i++)
	{
		var pos: Vector3 = new Vector3(Random.Range(-rangeFromOrigin,rangeFromOrigin), 
			startingElevationAboveGround, Random.Range(-rangeFromOrigin,rangeFromOrigin));
		Instantiate(crow, pos, Quaternion.identity);
	}
}