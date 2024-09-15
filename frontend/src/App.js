import { HubConnectionBuilder } from '@microsoft/signalr';
import WaitingRoom from './components/WaitingRoom';
import { useState } from 'react';
import Chat from './components/Chat';

function App() {
    const [connection, setConnection] = useState(null);
    const [chatRoom, setChatRoom] = useState('');
    const [messages, setMessages] = useState([]);

    const joinChat = async (userName, chatRoom) => {
        var connection = new HubConnectionBuilder()
            .withUrl('http://localhost:5140/chat')
            .withAutomaticReconnect()
            .build();

        connection.on('RecieveMessage', (userName, message) => {
            setMessages((messages) => [...messages, { userName, message }]);
        });

        try {
            await connection.start();
            await connection.invoke('JoinChat', { userName, chatRoom });

            setConnection(connection);
            setChatRoom(chatRoom);
        } catch (err) {
            console.error(err);
        }
    };

    const sendMessage = (message) => {
        connection.invoke('SendMessage', message);
    };

    const closeChat = async () => {
        await connection.stop();
        setConnection(null);
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-100">
            {connection ? (
                <Chat
                    messages={messages}
                    chatRoom={chatRoom}
                    closeChat={closeChat}
                    sendMessage={sendMessage}
                />
            ) : (
                <WaitingRoom joinChat={joinChat} />
            )}
        </div>
    );
}

export default App;
