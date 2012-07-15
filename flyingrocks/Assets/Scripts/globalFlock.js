var crow: GameObject;
var numberOfBirds = 25;

function Start()
{
	//create crows
	for(var i = 0; i < numberOfBirds; i++)
	{
		var pos: Vector3 = new Vector3(Random.Range(-10,10),5,Random.Range(-10,10));
		Instantiate(crow, pos, Quaternion.identity);
	}
}