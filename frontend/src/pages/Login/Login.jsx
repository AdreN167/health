import styles from "./Login.module.less";
import { useContext, useState } from "react";
import { signIn } from "../../api/authService";
import AuthContext from "../../store/AuthContext";
import {
  Button,
  FormControl,
  IconButton,
  Input,
  InputAdornment,
  InputLabel,
  TextField,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { routes } from "../../common/constants.jsx";

const Login = () => {
  const navigate = useNavigate();
  const ctx = useContext(AuthContext);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const handleMouseUpPassword = (event) => {
    event.preventDefault();
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const { accessToken } = await signIn({
        email: email,
        password: password,
      });
      ctx.login(accessToken);
      if (accessToken) {
        navigate(
          routes.map((l) => {
            if (l.path === "/product") return l.path;
          })[0]
        );
      }
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div className={styles.login}>
      <form className={styles.login__form}>
        <TextField
          sx={{ marginBottom: 4 }}
          label="Почта"
          variant="standard"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <FormControl variant="standard" sx={{ marginBottom: 4 }}>
          <InputLabel>Password</InputLabel>
          <Input
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            type={showPassword ? "text" : "password"}
            endAdornment={
              <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  onMouseUp={handleMouseUpPassword}
                >
                  {showPassword ? <VisibilityOff /> : <Visibility />}
                </IconButton>
              </InputAdornment>
            }
          />
        </FormControl>
        <Button variant="contained" onClick={handleLogin}>
          Войти
        </Button>
      </form>
    </div>
  );
};

export default Login;
