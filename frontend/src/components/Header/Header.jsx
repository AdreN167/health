import { Button } from "@mui/material";
import styles from "./Header.module.less";
import TextButton from "../TextButton/TextButton";
import { useContext } from "react";
import AuthContext from "../../store/AuthContext";

const Header = () => {
  const { logout } = useContext(AuthContext);

  return (
    <header className={styles.header}>
      <h1 className={styles.header__caption}>HEALTH</h1>
      <TextButton className={styles.header__btn} onClick={(e) => logout()}>
        Выйти
      </TextButton>
    </header>
  );
};

export default Header;
