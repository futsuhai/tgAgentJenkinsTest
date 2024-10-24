class ChatEventListener {
    constructor() {
        this.triggerStreamBtn = document.querySelector('#btn-trigger-stream');
        this.triggerStreamBtn.addEventListener("click", () => this.startListen())
        this.instanceId = null
    }

    async startListen() {
        if(this.instanceId === null)
            throw new Error('Please define this.instanceId in listener.js constructor.')
        
        const hubConnection = new signalR
            .HubConnectionBuilder()
            .withUrl('https://localhost:7131/apiTg/StartReceive')
            .build();

        
        try {
            await hubConnection.start();

            await hubConnection.stream("StartReceive", this.instanceId).subscribe({
                next: (item) => {
                    console.log(item);
                },
                error: (e) => {
                    console.log("Error " + e);
                    hubConnection.stop();
                }
            })

        } catch (e) {
            console.log("No connection " + e);
            this.triggerStreamBtn.setEnable();
        }
    }
}
