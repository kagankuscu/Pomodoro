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
            .navigationTitle(String(localized: "app-name", comment: "App name"))
            .toolbar {
                ShareLink(item: String(localized: "congratulation-string"))
                    .disabled(!vm.isShare)
            }
            .toolbarColorScheme(.dark, for: .navigationBar)
            .toolbarBackground(Color.red, for: .navigationBar)
            .toolbarBackground(.visible, for: .navigationBar)
            .onReceive(time) { _ in
                vm.updateCountDown()
            }
            .alert(String(localized: "congratulation-string") ,isPresented: $vm.isCompleted) {
                // action
            }
        }
    }
    @ViewBuilder
    func ButtonContainer() -> some View {
        HStack {
            MyButton(title: String(localized: "start-string")) {
                // action
                print("Start")
                print("\(vm.minutes)")
                vm.start(minutes: 25.0)
            }
            .disabled(vm.isActive)
            MyButton(title: String(localized: "skip-string")) {
                // action
                print("Skip")
                vm.skip()
            }
            .opacity(vm.isActive ? 1 : 0)
            MyButton(title: String(localized: "reset-string")) {
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
        Group {
            ContentView()
                .environment(\.locale, .init(identifier: "en"))
            ContentView()
                .environment(\.locale, .init(identifier: "tr"))
        }
    }
}
