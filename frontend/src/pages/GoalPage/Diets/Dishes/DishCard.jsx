import { Box, Stack, styled, Typography } from "@mui/material";
import HoveringCard from "../../../../components/HoveringCard/HoveringCard";
import { apiBaseUrl } from "../../../../common/constants";
import Image from "mui-image";

const ScrollOverflowBox = styled(Box)(({ theme }) => ({
  position: "relative",
  padding: 16,
  paddingRight: 32,
  boxShadow: "inset 0 0 10px rgba(0, 0, 0, 0.2)",
  borderRadius: 10,
  overflow: "hidden",
  "&:hover": {
    overflowY: "auto", // Показываем вертикальный скроллбар при наведении
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

const DishCard = ({
  id,
  name,
  description,
  calories,
  proteins,
  fats,
  carbohydrates,
  products,
  imageUrl,
  onClick,
}) => {
  return (
    <HoveringCard
      onClick={() => onClick(id)}
      sx={{ maxWidth: "auto", width: "80%" }}
      scale={1.01}
    >
      <Stack direction="row" gap={4}>
        <Stack sx={{ width: "30%" }}>
          <Box width={268} height={150}>
            <Image
              src={apiBaseUrl + imageUrl}
              style={{ objectFit: "contain" }}
            />
          </Box>
          <Typography variant="h5" sx={{ marginTop: 4 }}>
            {name}
          </Typography>
          <Stack direction="row" sx={{ marginTop: "auto" }}>
            <Stack>
              <Typography variant="body1">Калории:</Typography>
              <Typography variant="body1">Белги: </Typography>
              <Typography variant="body1">Жиры: </Typography>
              <Typography variant="body1">Углеводы:</Typography>
            </Stack>
            <Stack sx={{ marginLeft: "auto" }}>
              <Typography variant="body1">{calories.toFixed(2)}</Typography>
              <Typography variant="body1">{proteins.toFixed(2)}</Typography>
              <Typography variant="body1">{fats.toFixed(2)}</Typography>
              <Typography variant="body1">
                {carbohydrates.toFixed(2)}
              </Typography>
            </Stack>
          </Stack>
        </Stack>
        <Stack sx={{ width: "70%" }}>
          <Typography variant="body1">Список продуктов</Typography>
          {products && products.length > 0 && (
            <ScrollOverflowBox
              sx={{
                marginTop: 2,
                width: "100%",
                maxWidth: 400,
                height: 126,
              }}
            >
              <ul style={{ paddingLeft: 16 }}>
                {products.map((product) => (
                  <li key={product.id}>
                    <Stack direction="row" justifyContent="space-between">
                      <Typography>{product.name}</Typography>
                      <Typography>{product.weight} гр</Typography>
                    </Stack>
                  </li>
                ))}
              </ul>
            </ScrollOverflowBox>
          )}
          <Typography variant="body1" sx={{ marginTop: 2 }}>
            Описание
          </Typography>
          <ScrollOverflowBox
            sx={{
              marginTop: 2,
              wordWrap: "break-word",
              overflowWrap: "break-word",
              height: 134,
            }}
          >
            {description}
          </ScrollOverflowBox>
        </Stack>
      </Stack>
    </HoveringCard>
  );
};

export default DishCard;
