//
//  ContentView.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 6.06.23.
//

import SwiftUI

struct ContentView: View {
    @StateObject private var vm = CounterDownViewModel()
    private let width = 300.0
    private let time = Timer.publish(every: 1, on: .main, in: .common).autoconnect()
    @State private var isShow = true
    
    var body: some View {
        NavigationStack {
            VStack {
                Spacer()
                
                HStack {
                    Image(systemName: vm.countSet >= 1 ? "circle.fill" : "circle")
                        .resizable()
                        .frame(width: 30, height: 30)
                    Image(systemName: vm.countSet >= 2 ? "circle.fill" : "circle")
                        .resizable()
                        .frame(width: 30, height: 30)
                    Image(systemName: vm.countSet >= 3 ? "circle.fill" : "circle")
                        .resizable()
                        .frame(width: 30, height: 30)
                    Image(systemName: vm.countSet >= 4 ? "circle.fill" : "circle")
                        .resizable()
                        .frame(width: 30, height: 30)
                }
                .foregroundColor(.red)
                .frame(width: width)
                .padding()
                
                Text(vm.time)
                    .font(.system(size: 80,design: .rounded))
                    .bold()
                    .padding()
                    .frame(width: width)
                    .background(.thinMaterial)
                    .cornerRadius(20)
                    .overlay(RoundedRectangle(cornerRadius: 20).stroke(.gray, lineWidth: 2.0))
                    
                Text(vm.breakName)
                    .font(.title)
                    .fontDesign(.rounded)
                    .frame(width: width)
                    .opacity(vm.breakName.isEmpty ? 0 : 1)
                
                Spacer()
                
                ButtonContainer()
                    .padding(.bottom, 40)
                    .frame(width: width)
            }
            .navigationTitle("Pomodoro")
            .toolbar {
                ShareLink(item: "Congratulation you complete a pomodoro")
                    .disabled(!vm.isShare)
            }
            .toolbarColorScheme(.dark, for: .navigationBar)
            .toolbarBackground(Color.red, for: .navigationBar)
            .toolbarBackground(.visible, for: .navigationBar)
            .onReceive(time) { _ in
                vm.updateCountDown()
            }
            .alert(isPresented: $vm.isCompleted) {
                // action
//                Alert(title: Text("Deneme"), message: Text("This is the message"), dismissButton: .cancel())
                Alert(title: Text("Deneme 2"), primaryButton: .default(Text("OK")) {
                    vm.isShare.toggle()
                }, secondaryButton: .cancel())
            }
        }
    }
    @ViewBuilder
    func ButtonContainer() -> some View {
        HStack {
            MyButton(title: "Start") {
                // action
                print("Start")
                print("\(vm.minutes)")
                vm.start(minutes: 25.0)
            }
            .disabled(vm.isActive)
            MyButton(title: "Skip") {
                // action
                print("Skip")
                vm.skip()
            }
            .opacity(vm.isActive ? 1 : 0)
            MyButton(title: "Reset") {
                // action
                print("Reset")
                vm.reset()
            }
            .disabled(!vm.isActive)
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
