    const canvas = document.getElementById("clock");
    const ctx = canvas.getContext("2d");

    ctx.save();

    // Uhrzentrum und Radius
    const  centerX = canvas.width / 2;
    const centerY = canvas.height / 2;
    const radius = 195

    // Funktion zeichnen der uhr
    function drawClock() {
      // Hintergrund löschen
      ctx.clearRect(0, 0, canvas.width, canvas.height);

      // Rand zeichnen
      ctx.beginPath();
      ctx.arc(centerX, centerY, radius, 0, Math.PI * 2);
      ctx.fillStyle = "white";
      ctx.fill();

      ctx.strokeStyle = "black";
      ctx.lineWidth = 5;
      ctx.stroke();

      // Mittelpunkt
      ctx.beginPath();
      ctx.arc(centerX, centerY, 5, 0, Math.PI * 2);
      ctx.fillStyle = "black";
      ctx.fill();
      ctx.closePath();

      // Stundenmarkierungen
      for (let i = 0; i < 12; i++) {
        const winkel = (i * Math.PI) / 6; // 30 grad pro stunde
        const x1 = centerX + (radius - 40) * Math.cos(winkel);
        const y1 = centerY + (radius -40) * Math.sin(winkel);
        const x2 = centerX + radius * Math.cos(winkel);
        const y2 = centerY + radius * Math.sin(winkel);

        ctx.beginPath();
        ctx.moveTo(x1, y1);
        ctx.lineTo(x2, y2);
        ctx.strokeStyle = "black";
        ctx.lineWidth = 4;
        ctx.stroke();
        ctx.closePath();
      }

      // minutenmarkierungen
      for (let i = 0; i < 60; i++) {
        const winkel = (i * Math.PI) / 30; // 30 / 5 = 6 grad pro minute
        if (i % 5 !== 0) {
        const x1 = centerX + (radius - 20) * Math.cos(winkel);
        const y1 = centerY + (radius -20) * Math.sin(winkel);
        const x2 = centerX + radius * Math.cos(winkel);
        const y2 = centerY + radius * Math.sin(winkel);

        ctx.beginPath();
        ctx.moveTo(x1, y1);
        ctx.lineTo(x2, y2);
        ctx.strokeStyle = "black";
        ctx.lineWidth = 4;
        ctx.stroke();
        ctx.closePath();
        }
      }

      // aktuelle zeit holen
      const now = new Date();
      const hours = (now.getUTCHours() + 2) % 24; // für schweizer zeit utc+2
      const minutes = now.getUTCMinutes();
      const seconds = now.getUTCSeconds();

      // winkel berechnen, konvertieren in radian für sinus und cosinus funktionen
      const hourwinkel = (hours * Math.PI) / 6 + (minutes * Math.PI) / 360; // 30 grad pro h + 0.5 grad pro min
      const minutewinkel = (minutes * Math.PI) / 30 + (seconds * Math.PI) / 1800; // 6 grad pro min + 0.1 grad pro sek
      const secondwinkel = (seconds * Math.PI) / 30; // 6 grad pro sek

      // Zeigerlängen
      const hourHandLength = radius * 0.6;
      const minuteHandLength = radius * 0.8;
      const secondHandLength = radius * 0.9;

      // stundenzeiger zeichnen
      ctx.beginPath();
      ctx.moveTo(centerX, centerY);
      ctx.lineTo(
        centerX + hourHandLength * Math.cos(hourwinkel),
        centerY + hourHandLength * Math.sin(hourwinkel)
      );
      ctx.lineWidth = 6;
      ctx.strokeStyle = "black";
      ctx.stroke();

      // minutenzeiger zeichnen
      ctx.beginPath();
      ctx.moveTo(centerX, centerY);
      ctx.lineTo(
        centerX + minuteHandLength * Math.cos(minutewinkel),
        centerY + minuteHandLength * Math.sin(minutewinkel)
      );
      ctx.lineWidth = 4;
      ctx.strokeStyle = "black";
      ctx.stroke();

      // sekundenzeiger zeichnen
      ctx.beginPath();
      ctx.moveTo(centerX, centerY);
      ctx.lineTo(
        centerX + secondHandLength * Math.cos(secondwinkel),
        centerY + secondHandLength * Math.sin(secondwinkel)
      );
      ctx.lineWidth = 2;
      ctx.strokeStyle = "red";
      ctx.stroke();
      ctx.closePath();
    }

    drawClock();
    setInterval(drawClock, 1000); // jede sekunde aktualisieren

    ctx.translate(centerX, centerY); // ursprung zum zentrum setzen
    ctx.rotate(-Math.PI / 2); // -90 grad drehen damit 12 uhr oben ist
    ctx.translate(-centerX, -centerY); // zurücksetzen zum alten ursprung