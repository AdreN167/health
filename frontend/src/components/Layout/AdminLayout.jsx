import { Link, useLocation, useNavigate } from "react-router-dom";
import Header from "../Header/Header.jsx";
import styles from "./Layout.module.less";
import { routes } from "../../common/constants.jsx";
import { useContext, useEffect } from "react";
import AuthContext from "../../store/AuthContext.jsx";

const AdminLayout = ({ children }) => {
  const { role } = useContext(AuthContext);
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    if (
      !routes
        .map((i) => {
          if (i.isForAdmin) return i.path;
        })
        .includes(location.pathname) &&
      role === "User"
    ) {
      navigate("/profile");
    }
  }, []);

  return (
    <div style={{ position: "fixed" }}>
      <Header />
      <main className={styles.layout}>
        <nav className={styles.layout__nav}>
          {routes.map(
            (link, index) =>
              link.isForAdmin && (
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
              )
          )}
        </nav>
        <div className={styles.layout__content}>{children}</div>
      </main>
    </div>
  );
};

export default AdminLayout;
