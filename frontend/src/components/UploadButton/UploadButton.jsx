import { UploadFile } from "@mui/icons-material";
import { Button } from "@mui/material";

const UploadButton = ({ children, onChange, fileName, reset }) => {
  return (
    <>
      <Button
        component="label"
        role={undefined}
        variant="contained"
        tabIndex={-1}
        startIcon={<UploadFile />}
      >
        {children}
        <input type="file" hidden onChange={onChange} accept="image/*" />
      </Button>
      {fileName && (
        <p style={{ alignSelf: "center", cursor: "pointer" }} onClick={reset}>
          {fileName}
        </p>
      )}
    </>
  );
};

export default UploadButton;
