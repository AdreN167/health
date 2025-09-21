import { Box, IconButton, Stack, Typography } from "@mui/material";
import Image from "mui-image";
import { grey } from "@mui/material/colors";
import CancelRoundedIcon from "@mui/icons-material/CancelRounded";
import { apiBaseUrl } from "../../../../common/constants";
import useInput from "../../../../hooks/useInput";
import DisabledTextField from "../../../../components/DisabledTextField/DisabledTextField";

const FoodCard = ({
  id,
  name,
  calories,
  proteins,
  fats,
  carbohydrates,
  imageUrl,
  weight,
  onClick,
  onWeightChange,
  onSelect = () => {},
  selected = false,
  disabled = false,
}) => {
  const weightInput = useInput(weight, (num) =>
    num > 0 ? { isValid: true, err: null } : { isValid: false, err: "" }
  );

  const handleChange = (e) => {
    if (Number(e.target.value) < 0) return;
    weightInput.onchange(e);
    onWeightChange(id, e.target.value);
  };

  const handleClick = (e) => {
    e.stopPropagation();
    onClick(id);
  };

  return (
    <Box
      sx={{
        position: "relative",
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between",
        bgcolor: grey[300],
        borderRadius: 2,
        boxShadow: !selected
          ? "0px 5px 6px -3px rgba(0, 0, 0, 0.2), 0px 9px 12px 1px rgba(0, 0, 0, 0.14), 0px 3px 16px 2px rgba(0, 0, 0, 0.12)"
          : "0px 5px 6px -3px rgba(0, 110, 0, 0.2), 0px 9px 12px 1px rgba(0, 110, 0, 0.14), 0px 3px 16px 2px rgba(0, 110, 0, 0.12)",
        padding: 2,
        width: "100%",
        maxWidth: 200,
        height: 250,
      }}
      onClick={() => onSelect(id)}
    >
      {!disabled && (
        <IconButton
          onClick={handleClick}
          sx={{ position: "absolute", top: -18, right: -18 }}
        >
          <CancelRoundedIcon style={{ color: "#D6989E" }} />
        </IconButton>
      )}

      <Box width={200 - 16 - 16} height={70}>
        <Image src={apiBaseUrl + imageUrl} style={{ objectFit: "contain" }} />
      </Box>
      <Typography variant="body2">{name}</Typography>
      <Stack direction="row">
        <Stack>
          <Typography variant="body2">Калории:</Typography>
          <Typography variant="body2">Белги: </Typography>
          <Typography variant="body2">Жиры: </Typography>
          <Typography variant="body2">Углеводы:</Typography>
        </Stack>
        <Stack sx={{ marginLeft: "auto", textAlign: "right" }}>
          <Typography variant="body2">{calories.toFixed(2)}</Typography>
          <Typography variant="body2">{proteins.toFixed(2)}</Typography>
          <Typography variant="body2">{fats.toFixed(2)}</Typography>
          <Typography variant="body2">{carbohydrates.toFixed(2)}</Typography>
        </Stack>
      </Stack>
      <Stack direction="row" alignItems="center" gap={2}>
        <Typography variant="body2">Вес:</Typography>
        <DisabledTextField
          disabled={disabled}
          type="number"
          size="small"
          value={weightInput.value}
          onChange={handleChange}
          error={weightInput.error !== null}
        />
        <Typography>гр</Typography>
      </Stack>
    </Box>
  );
};

export default FoodCard;
