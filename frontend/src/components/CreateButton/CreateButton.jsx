import { green } from "@mui/material/colors";
import { styled } from "@mui/material/styles";
import { IconButton } from "@mui/material";

const CreateButton = styled(IconButton)(({ theme }) => ({
  width: "56px",
  height: "56px",
  borderRadius: 5,
  color: "white",
  backgroundColor: green["400"],
  "&:hover": {
    backgroundColor: green["700"],
  },
}));

export default CreateButton;
