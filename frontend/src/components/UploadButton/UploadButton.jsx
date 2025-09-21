import { UploadFile } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useRef } from "react";

const UploadButton = ({ children, onChange, fileName, reset }) => {
  const inputRef = useRef(null);

  const handleReset = (e) => {
    inputRef.current.value = null;
    reset();
  };

  return (
    <>
      <Button
        component="label"
        role={undefined}
        variant="contained"
        tabIndex={-1}
        startIcon={<UploadFile />}
        sx={{ height: 56 }}
      >
        {children}
        <input
          ref={inputRef}
          type="file"
          hidden
          onChange={onChange}
          accept="image/*"
        />
      </Button>
      {fileName && (
        <p
          style={{ alignSelf: "center", cursor: "pointer" }}
          onClick={handleReset}
        >
          {fileName}
        </p>
      )}
    </>
  );
};

export default UploadButton;
