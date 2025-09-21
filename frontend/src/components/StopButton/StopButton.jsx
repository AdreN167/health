import { green } from "@mui/material/colors";
import { styled } from "@mui/material/styles";
import { Button } from "@mui/material";

const StopButton = styled(Button)(({ theme }) => ({
  width: "80px",
  height: "32px",
  borderRadius: 5,
  color: "white",
  backgroundColor: "#D6989E",
  "&:hover": {
    backgroundColor: "#DA6671",
  },
}));

export default StopButton;
