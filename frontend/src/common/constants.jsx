import DishPage from "../pages/DishPage/DishPage";
import ExercisePage from "../pages/ExercisePage/ExercisePage";
import GoalPage from "../pages/GoalPage/GoalPage";
import ProductPage from "../pages/ProductPage/ProductPage";
import ProfilePage from "../pages/ProfilePage/ProfilePage";
import TrainerPage from "../pages/TrainerPage/TrainerPage";

export const apiBaseUrl = "http://localhost:5140";

export const routes = [
  {
    isDefaultForAdmin: true,
    isForAdmin: true,
    path: "/product",
    label: "Продукты",
    element: () => <ProductPage />,
  },
  {
    isForAdmin: true,
    path: "/dish",
    label: "Блюда",
    element: () => <DishPage />,
  },
  {
    isForAdmin: true,
    path: "/trainer",
    label: "Тренажеры",
    element: () => <TrainerPage />,
  },
  {
    isForAdmin: true,
    path: "/exercise",
    label: "Упражнения",
    element: () => <ExercisePage />,
  },
  {
    isDefaultForUser: true,
    path: "/profile",
    label: "Профиль",
    element: () => <ProfilePage />,
  },
  {
    path: "/goal",
    label: "Цели",
    element: () => <GoalPage />,
  },
];
