var orbspot : Texture;
var playerPos : Transform;
private var mapScale = 5;

private var radarSpotX: float;
private var radarSpotY: float;

private var radarWidth = 200;
private var radarHeight = 200;

function OnGUI () 
{
    GUI.BeginGroup (Rect (10, Screen.height - radarHeight - 10, radarWidth, radarHeight));
    	GUI.Box (Rect (0, 0, radarWidth, radarHeight), "Radar");
    	DrawSpotsForOrbs();
    GUI.EndGroup();
}

function DrawRadarBlip(go, spotTexture)
{
    var gameObjPos = go.transform.position;
    
    //find distance between object and player
    var dist = Vector3.Distance(playerPos.position, gameObjPos);
    
    //find the horizontal distances along the x and z between player and object
    var dx = playerPos.position.x - gameObjPos.x;
    var dz = playerPos.position.z - gameObjPos.z;
   
    //determine the angle of rotation between the direction the player is facing and the location
    //of the object
    deltay = Mathf.Atan2(dx, dz) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;

	//orient the object on the radar according to the direction the player is facing
    radarSpotX = dist * Mathf.Cos(deltay * Mathf.Deg2Rad) * mapScale;
    radarSpotY = dist * Mathf.Sin(deltay * Mathf.Deg2Rad) * mapScale;
   
    //draw a spot on the radar
    GUI.DrawTexture(Rect(radarWidth/2.0 + radarSpotX, radarHeight/2.0 + radarSpotY, 2, 2), spotTexture);
}

function DrawSpotsForOrbs()
{
    var gos : GameObject[];
    //look for all objects with a tag of orb
    gos = GameObject.FindGameObjectsWithTag("Seagull");

    var distance = Mathf.Infinity;
    var position = transform.position;

    for (var go : GameObject in gos)  
    {
	   DrawRadarBlip(go,orbspot);
    }
}
