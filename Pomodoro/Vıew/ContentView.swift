//
//  ContentView.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 6.06.23.
//

import SwiftUI

struct ContentView: View {
    var body: some View {
        NavigationStack {
            VStack {
                Spacer()
                
                Text("00:00")
                    .font(.system(size: 80))
                    .bold()
                
                Spacer()
                
                ButtonContainer()
                    .padding(.bottom, 40)
            }
            .navigationTitle("Pomodoro")
            .toolbar {
                Button {
                    // Action
                } label: {
                    Image(systemName: "square.and.arrow.up")
                }
            }
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
