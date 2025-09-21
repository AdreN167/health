import { Box, Stack, Typography } from "@mui/material";
import HoveringCard from "../../../../components/HoveringCard/HoveringCard";
import { apiBaseUrl } from "../../../../common/constants";
import Image from "mui-image";

const ProductCard = ({
  id,
  name,
  calories,
  proteins,
  fats,
  carbohydrates,
  imageUrl,
  onClick,
}) => {
  return (
    <HoveringCard onClick={() => onClick(id)}>
      <Box width={268} height={150}>
        <Image src={apiBaseUrl + imageUrl} style={{ objectFit: "contain" }} />
      </Box>
      <Typography variant="h6">{name}</Typography>
      <Stack direction="row">
        <Stack>
          <Typography variant="body2">Калории:</Typography>
          <Typography variant="body2">Белги: </Typography>
          <Typography variant="body2">Жиры: </Typography>
          <Typography variant="body2">Углеводы:</Typography>
        </Stack>
        <Stack sx={{ marginLeft: "auto" }}>
          <Typography variant="body2">{calories.toFixed(2)}</Typography>
          <Typography variant="body2">{proteins.toFixed(2)}</Typography>
          <Typography variant="body2">{fats.toFixed(2)}</Typography>
          <Typography variant="body2">{carbohydrates.toFixed(2)}</Typography>
        </Stack>
      </Stack>
    </HoveringCard>
  );
};

export default ProductCard;
