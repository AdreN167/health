import { Box } from "@mui/material";
import { grey } from "@mui/material/colors";
import { useState } from "react";

const HoveringCard = ({ children, sx, onClick, scale }) => {
  const [isHovering, setIsHovering] = useState(false);

  return (
    <Box
      onMouseEnter={() => setIsHovering(true)}
      onMouseLeave={() => setIsHovering(false)}
      onClick={onClick}
      sx={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "space-between",
        bgcolor: grey[300],
        borderRadius: 2,
        boxShadow: 9,
        padding: 2,
        width: "100%",
        maxWidth: 300,
        height: 400,
        cursor: "pointer",
        transition: "scale 0.2s",
        scale: isHovering ? (scale ? scale : 1.03) : 1,
        ...sx,
      }}
    >
      {children}
    </Box>
  );
};

export default HoveringCard;
