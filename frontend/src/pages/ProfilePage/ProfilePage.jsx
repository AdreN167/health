import { Stack, TextField, Typography, IconButton } from "@mui/material";
import EditOutlinedIcon from "@mui/icons-material/EditOutlined";
import UserLayout from "../../components/Layout/UserLayout";
import useInput from "../../hooks/useInput";
import { useContext, useEffect, useState } from "react";
import AuthContext from "../../store/AuthContext";
import { getUsersInfo, updateUsersInfo } from "../../api/userService";
import DietJournal from "./DietJournal";
import WorkoutJournal from "./WorkoutJournal";
import SaveToPdf from "../../components/SaveToPdf/SaveToPdf";
import { getDietEvents } from "../../api/dietEventJournalService";
import { getGoalsByUserEmail } from "../../api/goalService";
import { getWorkoutEvents } from "../../api/workoutEventJournalService";
import { getWorkouts } from "../../api/workoutService";
import OnlyTables from "./OnlyTables";
import { getDiets } from "../../api/dietService";
import PdfContext, { PdfProvider } from "../../store/PdfContext";
import DownloadRoundedIcon from "@mui/icons-material/DownloadRounded";
import { blue } from "@mui/material/colors";

const errMsg = "Ошибка ввода";
const validateNumber = (num) =>
  num > 0 ? { isValid: true, err: null } : { isValid: false, err: errMsg };

const ProfilePage = () => {
  const { token, email } = useContext(AuthContext);
  const [isSomethingChanged, setIsSomethingChanged] = useState();
  const age = useInput("", validateNumber);
  const height = useInput("", validateNumber);
  const weight = useInput("", validateNumber);

  const [diets, setDiets] = useState([]);
  const [dietEvents, setDietEvents] = useState([]);

  const [workouts, setWorkouts] = useState([]);
  const [workoutEvents, setWorkoutEvents] = useState([]);

  const [workoutsResult, setWorkoutsResult] = useState([]);
  const [dietsResult, setDietsResult] = useState([]);

  const [isSave, setIsSave] = useState(false);

  const getDietEventsAsync = async () => {
    try {
      setDietEvents((await getDietEvents(token, email)) ?? []);
    } catch (err) {
      console.log(err);
    }
  };

  const getAllDiets = async () => {
    try {
      const allDiets = [];
      const goals = await getGoalsByUserEmail(token, email);
      for (let i = 0; i < goals.length; i++) {
        allDiets.push.apply(allDiets, await getDiets(token, goals[i].id));
      }
      setDiets(allDiets);
    } catch (err) {
      console.log(err);
    }
  };

  const getWorkoutEventsAsync = async () => {
    try {
      setWorkoutEvents((await getWorkoutEvents(token, email)) ?? []);
    } catch (err) {
      console.log(err);
    }
  };

  const getAllWorkouts = async () => {
    try {
      const allWorkouts = [];
      const goals = await getGoalsByUserEmail(token, email);
      for (let i = 0; i < goals.length; i++) {
        allWorkouts.push.apply(
          allWorkouts,
          await getWorkouts(token, goals[i].id)
        );
      }
      setWorkouts(allWorkouts);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    const getInfo = async () => {
      try {
        var info = await getUsersInfo(token, email);
        age.setValue(info.age);
        height.setValue(info.height);
        weight.setValue(info.weight);
      } catch (err) {
        console.log(err);
      }
    };
    getInfo();
    getDietEventsAsync();
    getAllDiets();
    getWorkoutEventsAsync();
    getAllWorkouts();
  }, []);

  const handleClickUpdate = async () => {
    try {
      await updateUsersInfo(token, {
        email: email,
        age: age.value,
        height: height.value,
        weight: weight.value,
      });
      setIsSomethingChanged(false);
    } catch (err) {
      console.log(err);
    }
  };

  const handleGetResultFromDietJournal = (data) => {
    if (isSave) {
      setDietsResult(data);
    }
  };

  const handleGetResultFromWorkoutJournal = (data) => {
    if (isSave) {
      setWorkoutsResult(data);
    }
  };

  const handleSaved = () => setIsSave(false);

  return (
    <UserLayout>
      <Stack direction="row">
        <Typography variant="h5" sx={{ fontWeight: 600, height: 40 }}>
          Текущие параметры
        </Typography>
        {isSomethingChanged && (
          <IconButton onClick={handleClickUpdate} sx={{ marginLeft: 2 }}>
            <EditOutlinedIcon />
          </IconButton>
        )}
        <IconButton
          sx={{
            marginLeft: "auto",
            marginRight: "36px",
            width: "56px",
            height: "56px",
            borderRadius: 2,
            color: "white",
            backgroundColor: blue["700"],
            "&:hover": {
              backgroundColor: blue["800"],
            },
          }}
          onClick={() => setIsSave(true)}
        >
          <DownloadRoundedIcon />
        </IconButton>
      </Stack>
      <Stack direction="row" sx={{ marginTop: 2 }}>
        <TextField
          sx={{ maxWidth: 120 }}
          label="Возраст"
          type="number"
          value={age.value}
          onChange={(e) => {
            age.onchange(e);
            setIsSomethingChanged(true);
          }}
          error={age.error !== null}
          helperText={age.error}
        />
        <TextField
          sx={{ maxWidth: 120, marginLeft: 2 }}
          label="Рост"
          type="number"
          value={height.value}
          onChange={(e) => {
            height.onchange(e);
            setIsSomethingChanged(true);
          }}
          error={height.error !== null}
          helperText={height.error}
        />
        <TextField
          sx={{ maxWidth: 120, marginLeft: 2 }}
          label="Вес"
          type="number"
          value={weight.value}
          onChange={(e) => {
            weight.onchange(e);
            setIsSomethingChanged(true);
          }}
          error={weight.error !== null}
          helperText={weight.error}
        />
      </Stack>
      <Stack sx={{ marginTop: 8 }}>
        <Typography variant="h5" sx={{ fontWeight: 600, height: 40 }}>
          Общий журнал тренировок
        </Typography>
        <WorkoutJournal
          sx={{ marginTop: 2 }}
          workoutsDate={workoutEvents}
          workouts={workouts}
          getResult={handleGetResultFromWorkoutJournal}
        />
      </Stack>
      <Stack sx={{ marginTop: 8, marginBottom: 16 }}>
        <Typography variant="h5" sx={{ fontWeight: 600, height: 40 }}>
          Общий журнал диет
        </Typography>
        <DietJournal
          sx={{ marginTop: 2 }}
          mealsDate={dietEvents}
          diets={diets}
          getResult={handleGetResultFromDietJournal}
        />
      </Stack>
      <SaveToPdf isSave={isSave} onSaved={handleSaved}>
        <OnlyTables diets={dietsResult} workouts={workoutsResult} />
      </SaveToPdf>
    </UserLayout>
  );
};

export default ProfilePage;
