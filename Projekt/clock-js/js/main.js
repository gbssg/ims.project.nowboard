    import { Draw } from './draw.js';
    import { Get }  from './get.js';
    
    const canvas = document.getElementById("clock");
    const ctx = canvas.getContext("2d");

    ctx.save();

    // Uhrzentrum und Radius
    const  centerX = canvas.width / 2;
    const centerY = canvas.height / 2;
    const radius = 195;    

    const drawer = new Draw(ctx, centerX, centerY, radius);
    const getter = new Get();

    // Funktion zeichnen der uhr
    function drawClock() {
      // Hintergrund löschen
      ctx.clearRect(0, 0, canvas.width, canvas.height);

      // Rand zeichnen
      drawer.rand();

      // stunden- und minutenmarkierungen zeichnen
      drawer.hourmark();
      drawer.minutemark();

      // mittelpunkt
      drawer.mittelpunkt();  

      // winkel holen
      const { hourwinkel, minutewinkel, secondwinkel, littlesecondwinkel, littleminutewinkel, littlehourwinkel } = getter.winkel();

      // stundenzeiger zeichnen
      drawer.hourpointer(hourwinkel);
      drawer.littlehourpointer(littlehourwinkel);

      // minutenzeiger zeichnen
      drawer.minutepointer(minutewinkel);
      drawer.littleminutepointer(littleminutewinkel);

      // sekundenzeiger zeichnen
      drawer.secondpointer(secondwinkel);
      drawer.littlesecondpointer(littlesecondwinkel);      
    }

    ctx.translate(centerX, centerY); // ursprung zum zentrum setzen
    ctx.rotate(-Math.PI / 2); // -90 grad drehen damit 12 uhr oben ist
    ctx.translate(-centerX, -centerY); // zurücksetzen zum alten ursprung

    setInterval(drawClock, 1000); // jede sekunde aktualisieren
    drawClock();