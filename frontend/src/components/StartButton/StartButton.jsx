import { green } from "@mui/material/colors";
import { styled } from "@mui/material/styles";
import { Button } from "@mui/material";

const StartButton = styled(Button)(({ theme }) => ({
  minWidth: "80px",
  height: "32px",
  borderRadius: 5,
  color: "white",
  backgroundColor: green["400"],
  "&:hover": {
    backgroundColor: green["700"],
  },
}));

export default StartButton;
