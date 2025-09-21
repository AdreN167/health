import styles from "./Login.module.less";
import { useContext, useEffect, useState } from "react";
import { signIn, signUp } from "../../api/authService";
import AuthContext from "../../store/AuthContext";
import {
  Button,
  FormControl,
  FormHelperText,
  IconButton,
  Input,
  InputAdornment,
  InputLabel,
  Stack,
  Tab,
  TextField,
  Typography,
} from "@mui/material";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { Navigate, useNavigate } from "react-router-dom";
import { routes } from "../../common/constants.jsx";
import { TabContext, TabList, TabPanel } from "@mui/lab";
import useInput from "../../hooks/useInput.js";
import StorageContext from "../../store/StorageContext.jsx";

const validateEmail = (email) => {
  let re =
    /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email) && email.trim() !== ""
    ? { isValid: true, err: null }
    : { isValid: false, err: "Некорректная почта" };
};

const validatePassword = (pass) => {
  const str = pass.trim();
  let err = null;
  if (str === "") {
    err = "Поле не может быть пустым";
  } else if (str.length < 8) {
    err = "Пароль должен содержать минимум 8 символов";
  }
  return {
    isValid: err === null,
    err: err,
  };
};

const validateConfirmPassword = (pass) => {
  const str = pass.trim();
  let err = null;
  if (str === "") {
    err = "Поле не может быть пустым";
  }
  return {
    isValid: err === null,
    err: err,
  };
};

const validateNumber = (num) =>
  Number(num) > 0 ? { isValid: true, err: null } : { isValid: false, err: "" };

const Login = () => {
  const navigate = useNavigate();
  const ctx = useContext(AuthContext);
  const { fetchExercises, fetchProducts, fetchDishes } =
    useContext(StorageContext);
  const email = useInput("", validateEmail);
  const password = useInput("", validatePassword);
  const confirmPassword = useInput("", validateConfirmPassword);
  const age = useInput("", validateNumber);
  const height = useInput("", validateNumber);
  const weight = useInput("", validateNumber);

  const [showPassword, setShowPassword] = useState(false);
  const [currentTab, setCurrentTab] = useState("signin");

  const handleChangeCurrentTab = (event, newValue) => {
    setCurrentTab(newValue);
    email.reset();
    password.reset();
    confirmPassword.reset();
    age.reset();
    height.reset();
    weight.reset();
  };

  if (ctx.isAuthenticated) {
    if (ctx.role === "Admin")
      return (
        <Navigate to={routes.find((route) => route.isDefaultForAdmin).path} />
      );
    else
      return (
        <Navigate to={routes.find((route) => route.isDefaultForUser).path} />
      );
  }

  const validateRegForm = () => {
    if (
      email.value === "" ||
      password.value === "" ||
      confirmPassword.value === "" ||
      age.value === "" ||
      weight.value === "" ||
      height.value === "" ||
      email.error !== null ||
      password.error !== null ||
      confirmPassword.error !== null ||
      age.error !== null ||
      weight.error !== null ||
      height.error !== null
    )
      return false;
    else return true;
  };

  const validateLoginForm = () => {
    if (
      email.value === "" ||
      password.value === "" ||
      email.error !== null ||
      password.error !== null
    )
      return false;
    else return true;
  };

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const handleMouseUpPassword = (event) => {
    event.preventDefault();
  };

  const login = async () => {
    const { accessToken, role } = await signIn({
      email: email.value,
      password: password.value,
    });
    ctx.login(accessToken, role, email.value);
    if (accessToken) {
      if (role === "Admin") {
        navigate(
          routes.map((l) => {
            if (l.path === "/product") return l.path;
          })[0]
        );
      } else {
        await fetchExercises(ctx.token);
        await fetchProducts(ctx.token);
        await fetchDishes(ctx.token);
        role === "User" &&
          navigate(
            routes.map((l) => {
              if (l.path === "/profile") return l.path;
            })[0]
          );
      }
    }
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      await login();
    } catch (err) {
      console.log(err);
      alert("Такой пользователь не зарегистрирован или неверный пароль!");
    }
  };

  const handleRegistration = async (e) => {
    e.preventDefault();
    if (!validateRegForm()) {
      alert("Не все поля формы заполнены");
      return;
    }
    try {
      await signUp({
        email: email.value,
        password: password.value,
        confirmPassword: confirmPassword.value,
        age: age.value,
        height: height.value,
        weight: weight.value,
      });
      login();
    } catch (err) {
      alert("Такой пользователь уже существует или пароли не совпадают!");
    }
  };

  return (
    <div className={styles.login}>
      <TabContext value={currentTab}>
        <TabList
          onChange={handleChangeCurrentTab}
          centered
          sx={{
            maxWidth: 400,
            width: "100%",
            margin: "0 auto",
            marginBottom: 2,
            "&*": { width: "100%" },
          }}
          variant="fullWidth"
        >
          <Tab label="Вход" value="signin" />
          <Tab label="Регистрация" value="signup" />
        </TabList>
        <TabPanel
          value="signin"
          sx={{
            padding: 0,
            display: "flex",
            justifyContent: "center",
            "&-hidden": { padding: 0 },
          }}
        >
          <form className={styles.login__form}>
            <TextField
              sx={{ marginBottom: 4 }}
              label="Почта"
              variant="standard"
              value={email.value}
              onChange={email.onchange}
              error={email.error !== null}
              helperText={email.error}
            />
            <FormControl variant="standard" sx={{ marginBottom: 4 }}>
              <InputLabel error={password.error !== null}>Пароль</InputLabel>
              <Input
                value={password.value}
                onChange={password.onchange}
                type={showPassword ? "text" : "password"}
                error={password.error !== null}
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
              <FormHelperText sx={{ color: "#d32f2f" }}>
                {password.error}
              </FormHelperText>
            </FormControl>
            <Button variant="contained" onClick={handleLogin}>
              Войти
            </Button>
          </form>
        </TabPanel>
        <TabPanel
          value="signup"
          sx={{
            padding: 0,
            display: "flex",
            justifyContent: "center",
          }}
        >
          <form className={styles.login__form}>
            <TextField
              sx={{ marginBottom: 4 }}
              label="Почта"
              variant="standard"
              value={email.value}
              onChange={email.onchange}
              error={email.error !== null}
              helperText={email.error}
            />
            <FormControl variant="standard" sx={{ marginBottom: 4 }}>
              <InputLabel error={password.error !== null}>Пароль</InputLabel>
              <Input
                value={password.value}
                onChange={password.onchange}
                type={showPassword ? "text" : "password"}
                error={password.error !== null}
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
              <FormHelperText sx={{ color: "#d32f2f" }}>
                {password.error}
              </FormHelperText>
            </FormControl>
            <FormControl variant="standard" sx={{ marginBottom: 4 }}>
              <InputLabel error={confirmPassword.error !== null}>
                Повтор пароля
              </InputLabel>
              <Input
                value={confirmPassword.value}
                onChange={confirmPassword.onchange}
                type={showPassword ? "text" : "password"}
                error={confirmPassword.error !== null}
              />
              <FormHelperText sx={{ color: "#d32f2f" }}>
                {confirmPassword.error}
              </FormHelperText>
            </FormControl>
            <Stack direction="row" justifyContent="space-between">
              <TextField
                variant="standard"
                sx={{ maxWidth: 60, marginBottom: 4 }}
                label="Возрас"
                type="number"
                value={age.value}
                onChange={age.onchange}
                error={age.error !== null}
                helperText={age.error}
              />
              <TextField
                variant="standard"
                sx={{ maxWidth: 60, marginBottom: 4 }}
                label="Рост"
                type="number"
                value={height.value}
                onChange={height.onchange}
                error={height.error !== null}
                helperText={height.error}
              />
              <TextField
                variant="standard"
                sx={{ maxWidth: 60, marginBottom: 4 }}
                label="Вес"
                type="number"
                value={weight.value}
                onChange={weight.onchange}
                error={weight.error !== null}
                helperText={weight.error}
              />
            </Stack>
            <Button variant="contained" onClick={handleRegistration}>
              Зарегистрироваться
            </Button>
          </form>
        </TabPanel>
      </TabContext>
    </div>
  );
};

export default Login;
