import { Link, useLocation } from "react-router-dom";
import Header from "../Header/Header";
import styles from "./AdminLayout.module.less";
import { routes } from "../../common/constants.jsx";

const AdminLayout = ({ children }) => {
  const location = useLocation();
  return (
    <>
      <Header />
      <main className={styles.layout}>
        <nav className={styles.layout__nav}>
          {routes.map((link, index) => (
            <Link
              className={
                styles.layout__navItem +
                " " +
                (location.pathname === link.path &&
                  styles.layout__activeNavItem)
              }
              to={link.path}
              key={index}
            >
              {link.label}
            </Link>
          ))}
        </nav>
        <div className={styles.layout__content}>{children}</div>
      </main>
    </>
  );
};

export default AdminLayout;
