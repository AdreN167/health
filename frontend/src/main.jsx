import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import "./index.css";
import "dayjs/locale/en-gb";
import { AuthProvider } from "./store/AuthContext.jsx";
import { BrowserRouter as Router } from "react-router-dom";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { StorageProvider } from "./store/StorageContext.jsx";
import { PdfProvider } from "./store/PdfContext.jsx";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <Router>
      <StorageProvider>
        <PdfProvider>
          <AuthProvider>
            <LocalizationProvider
              dateAdapter={AdapterDayjs}
              adapterLocale="en-gb"
            >
              <App />
            </LocalizationProvider>
          </AuthProvider>
        </PdfProvider>
      </StorageProvider>
    </Router>
  </StrictMode>
);
