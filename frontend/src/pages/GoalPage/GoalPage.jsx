import { useContext, useEffect, useState } from "react";
import UserLayout from "../../components/Layout/UserLayout";
import CreateButton from "../../components/CreateButton/CreateButton";
import AddIcon from "@mui/icons-material/Add";
import { Stack, Typography } from "@mui/material";
import {
  deleteGoal,
  getGoals,
  getGoalsByUserEmail,
} from "../../api/goalService";
import GoalCard from "./GoalCard";
import AuthContext from "../../store/AuthContext";
import CreateUpdateGoal from "./CreateUpdateGoal";
import { WorkoutsProvider } from "../../store/Workouts/WorkoutsContext";
import { DietsProvider } from "../../store/Diets/DietsContext";
import Goal from "./Goal";

const GoalPage = () => {
  const { token, email } = useContext(AuthContext);
  const [fetchedData, setFetchedData] = useState([]);
  const [currentGoalInUpdate, setCurrentGoalInUpdate] = useState(null);
  const [currentGoal, setCurrentGoal] = useState(null);
  const [isAddingGoal, setIsAddingGoal] = useState(false);

  const get = async () => {
    try {
      const data = await getGoalsByUserEmail(token, email);
      setFetchedData(data);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    get();
  }, [isAddingGoal, currentGoalInUpdate, currentGoal]);

  const handleClickAddButton = (e) => {
    setIsAddingGoal(true);
  };

  const handleClickEditGoal = (goal) => {
    setCurrentGoalInUpdate(goal);
  };

  const handleClickDeleteGoal = async (goal) => {
    try {
      await deleteGoal(token, goal.id);
      await get();
    } catch (err) {
      console.log(err);
    }
  };

  const handleClickGoalCard = (goal) => {
    setCurrentGoal(goal);
  };

  const handleClickBack = () => {
    setCurrentGoalInUpdate(null);
    setCurrentGoal(null);
    setIsAddingGoal(false);
  };

  return (
    <UserLayout>
      <WorkoutsProvider>
        <DietsProvider>
          {isAddingGoal ? (
            <CreateUpdateGoal isAdding={true} onClickBack={handleClickBack} />
          ) : currentGoalInUpdate ? (
            <CreateUpdateGoal
              goal={currentGoalInUpdate}
              onClickBack={handleClickBack}
            />
          ) : currentGoal ? (
            <Goal goal={currentGoal} onClickBack={handleClickBack} />
          ) : (
            <>
              <CreateButton
                sx={{ marginLeft: "auto", marginRight: 2 }}
                onClick={handleClickAddButton}
              >
                <AddIcon />
              </CreateButton>
              Добавить цель
              <Stack
                direction="column"
                gap={2}
                sx={{ marginTop: 2 }}
                justifyContent="center"
              >
                {fetchedData.length !== 0 ? (
                  fetchedData.map((goal) => (
                    <GoalCard
                      key={goal.id}
                      content={goal}
                      onClick={handleClickGoalCard}
                      onEditClick={handleClickEditGoal}
                      onDeleteClick={handleClickDeleteGoal}
                    />
                  ))
                ) : (
                  <Typography color="gray" sx={{ marginTop: 2 }}>
                    У вас пока нет целей...
                  </Typography>
                )}
              </Stack>
            </>
          )}
        </DietsProvider>
      </WorkoutsProvider>
    </UserLayout>
  );
};

export default GoalPage;
