import { styled, TextField } from "@mui/material";

const DisabledTextField = styled(TextField)(({ theme }) => ({
  "& .Mui-disabled": {
    opacity: 1, // убираем прозрачность
    WebkitTextFillColor: "rgba(0, 0, 0, 1)",
  },
  "& .MuiInputBase-root.Mui-disabled": {
    opacity: 1, // убираем прозрачность
    WebkitTextFillColor: "rgba(0, 0, 0, 1)",
  },
  "&.MuiInputBase-input-MuiOutlinedInput-input .Mui-disabled": {
    opacity: 1, // убираем прозрачность
    WebkitTextFillColor: "rgba(0, 0, 0, 1)",
  },
}));

export default DisabledTextField;
