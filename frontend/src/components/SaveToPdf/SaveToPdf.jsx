import React, { useEffect, useState } from "react";
import html2canvas from "html2canvas";
import jsPDF from "jspdf";

const SaveToPdf = ({ children, isSave, onSaved }) => {
  const [isVisible, setIsVisible] = useState(false);

  useEffect(() => {
    if (!isSave) return;
    handleDownloadPDF();
  }, [isSave]);

  const handleDownloadPDF = () => {
    setIsVisible(true); // Показываем элемент перед рендерингом

    setTimeout(() => {
      const input = document.getElementById("pdf-content");
      html2canvas(input).then((canvas) => {
        const imgData = canvas.toDataURL("image/png");
        const pdf = new jsPDF("p", "mm", "a4");
        const imgWidth = 210;
        const pageHeight = 295;
        const imgHeight = (canvas.height * imgWidth) / canvas.width;
        let heightLeft = imgHeight;

        let position = 0;

        pdf.addImage(imgData, "PNG", 0, position, imgWidth, imgHeight);
        heightLeft -= pageHeight;

        while (heightLeft >= 0) {
          position = heightLeft - imgHeight;
          pdf.addPage();
          pdf.addImage(imgData, "PNG", 0, position, imgWidth, imgHeight);
          heightLeft -= pageHeight;
        }

        pdf.save("downloaded-file.pdf");

        setIsVisible(false); // Скрываем элемент после завершения
        onSaved(); // Вызываем onSaved после завершения
      });
    }, 100);
  };

  return (
    <div>
      <div id="pdf-content" style={{ display: isVisible ? "block" : "none" }}>
        {children}
      </div>
    </div>
  );
};

export default SaveToPdf;
