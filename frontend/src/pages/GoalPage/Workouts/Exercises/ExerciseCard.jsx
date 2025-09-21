import { Box, Typography } from "@mui/material";
import Image from "mui-image";
import { apiBaseUrl } from "../../../../common/constants";
import HoveringCard from "../../../../components/HoveringCard/HoveringCard";

const ExerciseCard = ({
  id,
  name,
  description,
  calories,
  imageUrl,
  onClick,
}) => {
  return (
    <HoveringCard onClick={() => onClick(id)}>
      <Box width={268} height={150}>
        <Image src={apiBaseUrl + imageUrl} style={{ objectFit: "contain" }} />
      </Box>
      <Typography variant="h6">{name}</Typography>
      <Typography
        sx={{
          marginBottom: 1,
          marginTop: 1,
          width: 268,
          height: 100,
          whiteSpace: "normal",
          wordBreak: "break-all",
          overflow: "hidden",
          textOverflow: "ellipsis",
        }}
      >
        {description}
      </Typography>
      <Typography variant="body2">Калории/повторение: {calories}</Typography>
    </HoveringCard>
  );
};

export default ExerciseCard;
