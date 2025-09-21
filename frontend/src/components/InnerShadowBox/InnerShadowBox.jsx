import { Box, styled } from "@mui/material";

const InnerShadowBox = styled(Box)(({ theme }) => ({
  position: "relative",
  padding: 24,
  overflow: "hidden",
  boxShadow: "inset 0 0 10px rgba(0, 0, 0, 0.2)",
  "&:hover": {
    overflowX: "auto", // Показываем вертикальный скроллбар при наведении
    scrollbarWidth: "thin", // Для Firefox
  },
  "&::-webkit-scrollbar": {
    // Для Chrome, Safari и Edge
    width: "0px",
  },
  "&:hover::-webkit-scrollbar": {
    width: "8px", // Ширина скроллбара при наведении
  },
  "&::-webkit-scrollbar-thumb": {
    backgroundColor: "rgba(0, 0, 0, 0.5)", // Цвет ползунка скроллбара
    borderRadius: "10px", // Закругление скроллбара
  },
}));

export default InnerShadowBox;
