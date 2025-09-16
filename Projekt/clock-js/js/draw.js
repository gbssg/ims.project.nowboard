// js/class.js
export class Draw {
    constructor(ctx, centerX, centerY, radius) {
        this.ctx = ctx;
        this.centerX = centerX;
        this.centerY = centerY;
        this.radius = radius;
    }

    hourmark() {
      const { ctx, centerX, centerY, radius } = this;
        for (let i = 0; i < 12; i++) {
            const winkel = (i * Math.PI) / 6;
            // abstand zwischen rand und markierungen
            const margin = 10;
            const x1 = centerX + (radius - 50) * Math.cos(winkel);
            const y1 = centerY + (radius - 50) * Math.sin(winkel);
            const x2 = centerX + (radius - margin) * Math.cos(winkel);
            const y2 = centerY + (radius - margin) * Math.sin(winkel);

            ctx.beginPath();
            ctx.moveTo(x1, y1);
            ctx.lineTo(x2, y2);
            ctx.strokeStyle = "black";
            ctx.lineWidth = 12;
            ctx.stroke();
            ctx.closePath();
        }
    }

    minutemark() {
        const { ctx, centerX, centerY, radius, innerradius } = this;
        for (let i = 0; i < 60; i++) {
            const winkel = (i * Math.PI) / 30;
            // abstand zwischen rand und markierungen
            const margin = 10;
            if (i % 5 !== 0) {
                const x1 = centerX + (radius - 25) * Math.cos(winkel);
                const y1 = centerY + (radius - 25) * Math.sin(winkel);
                const x2 = centerX + (radius - margin) * Math.cos(winkel);
                const y2 = centerY + (radius - margin) * Math.sin(winkel);

                ctx.beginPath();
                ctx.moveTo(x1, y1);
                ctx.lineTo(x2, y2);
                ctx.strokeStyle = "black";
                ctx.lineWidth = 5;
                ctx.stroke();
                ctx.closePath();
            }
        }
    }

    hourpointer(hourwinkel) {
        const { ctx, centerX, centerY, radius } = this;
        const hourHandLength = radius * 0.65;
        // stundenzeiger zeichnen
      ctx.beginPath();
      ctx.moveTo(centerX, centerY);
      ctx.lineTo(
        centerX + hourHandLength * Math.cos(hourwinkel),
        centerY + hourHandLength * Math.sin(hourwinkel)
      );
      ctx.lineWidth = 15;
      ctx.strokeStyle = "black";
      ctx.stroke();
    }

    minutepointer(minutewinkel) {
        const { ctx, centerX, centerY, radius } = this;
        const minuteHandLength = radius * 0.949;
        // minutenzeiger zeichnen
      ctx.beginPath();
      ctx.moveTo(centerX, centerY);
      ctx.lineTo(
        centerX + minuteHandLength * Math.cos(minutewinkel),
        centerY + minuteHandLength * Math.sin(minutewinkel)
      );
      ctx.lineWidth = 12;
      ctx.strokeStyle = "black";
      ctx.stroke();
    }

    secondpointer(secondwinkel) {
        const { ctx, centerX, centerY, radius } = this;
        const secondHandLength = radius * 0.6;
        // sekundenzeiger zeichnen
        ctx.beginPath();
      ctx.moveTo(centerX, centerY);
      ctx.lineTo(
        centerX + secondHandLength * Math.cos(secondwinkel),
        centerY + secondHandLength * Math.sin(secondwinkel)
      );
      ctx.lineWidth = 3;
      ctx.strokeStyle = "#c33334";
      ctx.stroke();

      // roter mittelpunkt
      ctx.beginPath();
      ctx.arc(centerX, centerY, 5, 0, Math.PI * 2);
      ctx.fillStyle = "#c33334";
      ctx.fill();
      ctx.closePath();

      ctx.beginPath(); // roten kreis am ende des zeigers
      // Ende des zeigers wird gebraucht, sprich für x = centerX + secondHandLength * cos(secondwinkel)
      // und für y = centerY + secondHandLength * sin(secondwinkel)
      ctx.beginPath();
      ctx.moveTo(centerX + secondHandLength, centerY + secondHandLength);
      ctx.lineTo()

      
    }

    rand() {
        const { ctx, centerX, centerY, radius } = this;
        // Rand zeichnen
      ctx.beginPath();
      ctx.arc(centerX, centerY, radius, 0, Math.PI * 2);
      ctx.fillStyle = "white";
      ctx.fill();

      ctx.strokeStyle = "silver";
      ctx.lineWidth = 7;
      ctx.stroke();
    }

    mittelpunkt() {
        const { ctx, centerX, centerY } = this;
        // mittelpunkt
      ctx.beginPath();
      ctx.arc(centerX, centerY, 7, 0, Math.PI * 2);
      ctx.fillStyle = "black";
      ctx.fill();
      ctx.closePath();
    }

    
}
