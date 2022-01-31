export async function showGltfFile(canvas, contentStreamReference) {

    const engine = new BABYLON.Engine(canvas, true); // Generate the BABYLON 3D engine

    // Add your code here matching the playground format

    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);

    const scene = createScene(engine, canvas, url); //Call the createScene function

    URL.revokeObjectURL(url);

    // Register a render loop to repeatedly render the scene
    engine.runRenderLoop(function () {
        scene.render();
    });

    // Watch for browser/canvas resize events
    window.addEventListener("resize", function () {
        engine.resize();
    });
}

// Add your code here matching the playground format
function createScene(engine,canvas, objectUrl) {

    const scene = new BABYLON.Scene(engine);

    BABYLON.SceneLoader.Append("", objectUrl, scene, function () {


    }, undefined, undefined, ".glb");

    const camera = new BABYLON.ArcRotateCamera("camera", -Math.PI / 2, Math.PI / 2.5, 15, new BABYLON.Vector3(0, 0, 0));
    camera.attachControl(canvas, true);
    const light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(1, 1, 0));

    return scene;
};