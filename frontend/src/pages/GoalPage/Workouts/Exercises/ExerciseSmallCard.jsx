import {
  Box,
  Chip,
  IconButton,
  Stack,
  TextField,
  Typography,
} from "@mui/material";
import Image from "mui-image";
import { apiBaseUrl } from "../../../../common/constants";
import { grey } from "@mui/material/colors";
import { useEffect, useRef, useState } from "react";
import CancelRoundedIcon from "@mui/icons-material/CancelRounded";
import useInput from "../../../../hooks/useInput";
import DisabledTextField from "../../../../components/DisabledTextField/DisabledTextField";

const ExerciseSmallCard = ({
  id,
  name,
  calories,
  imageUrl,
  count,
  onClick,
  onCountChange,
  disabled = false,
}) => {
  const countInput = useInput(count, (num) =>
    num > 0 ? { isValid: true, err: null } : { isValid: false, err: "" }
  );

  const handleChange = (e) => {
    if (Number(e.target.value) < 0) return;
    countInput.onchange(e);
    onCountChange(id, e.target.value);
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
        boxShadow: 9,
        padding: 2,
        width: "100%",
        maxWidth: 200,
        height: 250,
      }}
    >
      {!disabled && (
        <IconButton
          onClick={() => onClick(id)}
          sx={{ position: "absolute", top: -18, right: -18 }}
        >
          <CancelRoundedIcon style={{ color: "#D6989E" }} />
        </IconButton>
      )}

      <Box width={200 - 16 - 16} height={70}>
        <Image src={apiBaseUrl + imageUrl} style={{ objectFit: "contain" }} />
      </Box>
      <Typography variant="body2">{name}</Typography>
      <Typography variant="body2">Калории/повторение: {calories}</Typography>
      <Stack direction="row" alignItems="center" gap={2}>
        <Typography variant="body2">Повторения:</Typography>
        <DisabledTextField
          disabled={disabled}
          type="number"
          size="small"
          value={countInput.value}
          onChange={handleChange}
          error={countInput.error !== null}
        />
      </Stack>
    </Box>
  );
};

export default ExerciseSmallCard;
